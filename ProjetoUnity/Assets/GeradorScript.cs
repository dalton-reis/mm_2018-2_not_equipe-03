using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class GeradorScript : MonoBehaviour {

    public GameObject[] listaPrefab;
    public GameObject caixa;
    public Transform player;
    public Camera cam;
    public Text points;
    public Text Question;
    public Text Vidas;
    public AudioClip success;
    public AudioClip fail;
    public AudioSource ring;
    public AudioSource bgSound;
    public bool mirroredMovement = false;
    public bool offsetRelativeToSensor = false;
    public KinectManager manager;
    public GameObject panelRestart;
    public GameObject panelStart;
    public Text pontuacaoFinal;

    private float m_Success;
    private float m_Fail;
    private float m_QuarterNote;
    private bool started = false;
    private int[] valoresSorteio = { 0, 0, 0, 0, 0, 0, 1, 1, 2, 2 };
    protected GameObject offsetNode;

    protected Transform bodyRoot;
    protected KinectManager kinectManager;
    protected int moveRate = 5;
    protected float xOffset, yOffset, zOffset;
	protected bool offsetCalibrated = false;

    Vector3 velocity;    
    float pontoX = 0.0f;
    int Score = 0;
    int primeiroValor, segundoValor, resp1, resp2, resp3;
    string sinal;
    string[] sinais = {"+", "-", "x"};

    int teste = 0;
    GameObject a1, a2, a3;
    List<GameObject> listaAtivos;
    int respostaCerta;
    int raia = 2;
    bool inicou = false;
    bool devePerguntar = true;

    int nivel = 1;
    int progress = 0;
    int vidas = 3;
	// Use this for initialization
	void Start () {
        velocity = new Vector3(18.0f, 0.0f, 0.0f);
        listaAtivos = new List<GameObject>();
        setScore(0);
        bodyRoot = transform;
    }

    void setScore(int score)  {
        Score = score;
        points.text = "Pontuação: " + score.ToString();
    }

    public void validarResultado(int resultado) {
        Question.gameObject.SetActive(false);
        if (resultado == respostaCerta)
        {
            ring.clip = success;
            ring.time = 1;
            ring.Play();
            setScore(Score + 10);
            progress++;

            if (Score > 0 && Score % 100 == 0)
                SetVidas(vidas + 1);

            if (progress == 5)
            {
                nivel++;
                progress = 0;
            }               

            velocity.x += 1.0f;
        } else
        {
            ring.clip = fail;
            ring.Play();
            SetVidas(vidas - 1);
        }

        if (resultado != a1.GetComponent<BoxScript>().getResult())
            Destroy(a1);

        if (resultado != a2.GetComponent<BoxScript>().getResult())
            Destroy(a2);

        if (resultado != a3.GetComponent<BoxScript>().getResult())
            Destroy(a3);
        
        devePerguntar = true;
    }

    public void SetVidas(int _vidas)
    {
        vidas = _vidas;
        Vidas.text = "Vidas: ";
        for (int i = 0; i < vidas; i++)
            Vidas.text = Vidas.text + "♥ ";

        if (vidas == 0)
        {
            bgSound.Stop();
            devePerguntar = false;
            pontuacaoFinal.text = "Pontuação: " + Score;
            panelRestart.SetActive(true);
        }
    }

    public void esquerda() {
        raia = 1;
    }

    public void centro()   {
        raia = 2;
    }

    public void direita()  {
        raia = 3;
    }

    public void Iniciar()
    {
        points.gameObject.SetActive(true);
        Vidas.gameObject.SetActive(true);
        panelStart.SetActive(false);

        started = true;
    }

    public void resetFase()
    {
        cam.transform.position = new Vector3(0.0f, 54.0f, 0.0f);
        player.transform.position = new Vector3(6.5f, 50.5f, 0.0f);
        pontoX = 0.0f;
        SetVidas(3);
        setScore(0);
        inicou = false;
        devePerguntar = true;
        panelRestart.SetActive(false);

        for (int i = 0; i < listaAtivos.Count; i++)
        {
            GameObject objDestroy = listaAtivos[0];
            listaAtivos.Remove(objDestroy);
            Destroy(objDestroy);
        }
    }
	
	// Update is called once per frame
	void Update () {            
        if ((raia == 1 && player.transform.position.z < 4.5f) || 
            (raia == 3 && player.transform.position.z > -4.5f) || 
            (raia == 2 && (player.transform.position.z > 0.1f || player.transform.position.z < -0.1f)))
        {
            if (raia == 2 && player.transform.position.z < 0.0f)
            {
                velocity.z = 18f;
            } else
            if (raia == 2 && player.transform.position.z > 0.0f)
            {
                velocity.z = -18f;
            } else
            {
                if (raia == 1)  
                {
                    velocity.z = 18f;
                } else
                if (raia == 3)  
                {
                    velocity.z = -18f;
                }
            }
        } else  {
            velocity.z = 0.0f;
        }

        if (manager.Player1Calibrated && vidas > 0 && started)
        {
            inicou = true;
            cam.transform.position += velocity * Time.fixedDeltaTime;
            player.transform.position += velocity * Time.fixedDeltaTime;

            if (!bgSound.isPlaying)
            {
                bgSound.Play();
            }
        }
        else
        {
            bgSound.Pause();
        }

        if (devePerguntar && cam.transform.position.x > 70.0f && vidas > 0 && started) 
            LancarPergunta();
            
        if (pontoX - cam.transform.position.x < (120.8f * 2))
        {
            int sorteioResult = Random.Range(0, 10);
            var prefabIndex = valoresSorteio[sorteioResult];
            GameObject obj = Instantiate(listaPrefab[prefabIndex], new Vector3(pontoX, 50.0f, 0.0f), Quaternion.Euler(new Vector3(0.0f, 0.0f, 0.0f)));
            pontoX += 120.8f;

            listaAtivos.Add(obj);
            if (listaAtivos.Count > 3) {
                GameObject objDestroy = listaAtivos[0];
                listaAtivos.Remove(objDestroy);
                Destroy(objDestroy);
            }
        }
    }

    void gerarValores() {
        int sinalRandom = Random.Range(0, 3);
        sinal = sinais[sinalRandom];

        switch(sinal)   {
            case "+":
                respostaCerta = Random.Range(2, nivel * 10);
                primeiroValor = Random.Range(1, respostaCerta);
                segundoValor = respostaCerta - primeiroValor;

                break;
            case "-":
                primeiroValor = Random.Range(2, nivel * 10);
                respostaCerta = Random.Range(1, primeiroValor);
                segundoValor = primeiroValor - respostaCerta;

                break;
            case "x":
                primeiroValor = Random.Range(1, 10);
                segundoValor = Random.Range(1, nivel);
                respostaCerta = primeiroValor * segundoValor;

                break;
        }

        List<int> excludes = new List<int>();
        excludes.Add(respostaCerta);
        int caixaRandom = Random.Range(1, 4);
        resp1 = 0;
        resp2 = 0;
        resp3 = 0;
        switch (caixaRandom) {
            case 1:
                resp1 = respostaCerta;
                resp2 = RandomRangedResposta(respostaCerta, excludes);

                excludes.Add(resp2);
                resp3 = RandomRangedResposta(respostaCerta, excludes);
                break;
            case 2:
                resp1 = RandomRangedResposta(respostaCerta, excludes);
                resp2 = respostaCerta;

                excludes.Add(resp1);
                resp3 = RandomRangedResposta(respostaCerta, excludes);
                break;
            case 3:
                resp1 = RandomRangedResposta(respostaCerta, excludes);

                excludes.Add(resp1);
                resp2 = RandomRangedResposta(respostaCerta, excludes);
                resp3 = respostaCerta;
                break;
        }
    }

    int RandomRangedResposta(int valor, List<int> exclude)  {
        int valorFinal = valor;

        while (exclude.Contains(valorFinal)) {   
            if (valor - 10 < 0)
                valorFinal = Random.Range(Random.Range(0, 5), valor + 10);
            else
            if (valor + 10 > 99)
                valorFinal = Random.Range(valor - 10, Random.Range(90, 99));
            else
                valorFinal = Random.Range(valor - 10, valor + 10);
        }

        return valorFinal;
    }

    void LancarPergunta()   {
        Question.gameObject.SetActive(true);
        devePerguntar = false;
        gerarValores();

        Question.text = "Quanto é " + primeiroValor.ToString() + " " + sinal + " " + segundoValor.ToString() + "?";

        teste += 1;

        Debug.Log(cam.transform.position.x);
        a1 = Instantiate(caixa, new Vector3(cam.transform.position.x + 150.0f + 10.0f, 49.90f,  4.7f), Quaternion.Euler(new Vector3( 4.0f, -4.0f, - 6.0f)));
        BoxScript box1 = a1.GetComponent<BoxScript>();
        box1.setResult(resp1);

        a2 = Instantiate(caixa, new Vector3(cam.transform.position.x + 150.0f + 5.0f, 49.92f,  0.0f), Quaternion.Euler(new Vector3( 0.0f,  0.0f, -10.0f)));
        BoxScript box2 = a2.GetComponent<BoxScript>();
        box2.setResult(resp2);

        a3 = Instantiate(caixa, new Vector3(cam.transform.position.x + 150.0f, 49.89f, -4.7f), Quaternion.Euler(new Vector3(-4.0f,  4.0f, - 0.0f)));
        BoxScript box3 = a3.GetComponent<BoxScript>();
        box3.setResult(resp3);
    }
}
