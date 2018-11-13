using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeradorScript : MonoBehaviour {

    public GameObject[] listaPrefab;
    public GameObject caixa;
    public Transform player;
    public Camera cam;
    public Text points;
    public Text Question;

    Vector3 velocity;    
    float pontoX = 106.0f;
    int Score = 0;
    int primeiroValor, segundoValor, resp1, resp2, resp3;
    string sinal;
    string[] sinais = {"+", "-", "*"};

    int teste = 0;
    GameObject a1, a2, a3;
    List<GameObject> listaAtivos;
    int respostaCerta;
    int raia = 2;
    bool devePerguntar = true;
	// Use this for initialization
	void Start () {
        velocity = new Vector3(38.0f, 0.0f, 0.0f);
        listaAtivos = new List<GameObject>();
        setScore(0);
    }

    void setScore(int score)  {
        Score = score;
        points.text = "Pontuação: " + score.ToString();
    }

    public void validarResultado(int resultado) {
        Question.gameObject.SetActive(false);
        if (resultado == respostaCerta)  {
            setScore(Score + 10);
        } else  {            
            setScore(Score - 10);
        }

        if (resultado != a1.GetComponent<BoxScript>().getResult())
            Destroy(a1);

        if (resultado != a2.GetComponent<BoxScript>().getResult())
            Destroy(a2);

        if (resultado != a3.GetComponent<BoxScript>().getResult())
            Destroy(a3);
        
        devePerguntar = true;
    }

    void esquerda() {
        raia = 1;
    }

    void centro()   {
        raia = 2;
    }

    void direita()  {
        raia = 3;
    }
	
	// Update is called once per frame
	void Update () {    
        if (Input.GetKey("a"))  
        {
            esquerda();
        } else
        if (Input.GetKey("s"))  
        {
            centro();
        } else
        if (Input.GetKey("d"))  
        {
            direita();
        } 

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

        cam.transform.position += velocity * Time.fixedDeltaTime;
        player.transform.position += velocity * Time.fixedDeltaTime;

        if (devePerguntar && cam.transform.position.x % 200 < 2.0f) 
            LancarPergunta();
            
        if ((pontoX - 106.0f) - cam.transform.position.x < (120.8f * 2))
        {
            GameObject obj = Instantiate(listaPrefab[0], new Vector3(pontoX, 50.0f, 0.0f), Quaternion.LookRotation(new Vector3(0.0f, 90.0f, 0.0f)));
            pontoX += 120.8f;

            listaAtivos.Add(obj);
            if (listaAtivos.Count > 3) {
                GameObject objDestroy = listaAtivos[0];
                listaAtivos.Remove(objDestroy);
                Destroy(objDestroy);
            }

            velocity.x += 1.0f;
        }
    }

    void gerarValores() {
        var sinalRandom = Random.Range(0, 2);
        sinal = sinais[0];

        switch(sinal)   {
            case "+":
                respostaCerta = Random.Range(0, 50);
                primeiroValor = Random.Range(0, respostaCerta);
                segundoValor = respostaCerta - primeiroValor;

                break;
        }

        List<int> excludes = new List<int>();
        excludes.Add(respostaCerta);
        var caixaRandom = Random.Range(1, 3);
        switch(caixaRandom) {
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

        Question.text = "Quanto é " + primeiroValor.ToString() + sinal + segundoValor.ToString() + "?";

        teste += 1;

        a1 = Instantiate(caixa, new Vector3(pontoX + 10.0f, 49.90f,  4.7f), Quaternion.Euler(new Vector3( 4.0f, -4.0f, - 6.0f)));
        BoxScript box1 = a1.GetComponent<BoxScript>();
        box1.setResult(resp1);

        a2 = Instantiate(caixa, new Vector3(pontoX + 5.0f, 49.92f,  0.0f), Quaternion.Euler(new Vector3( 0.0f,  0.0f, -10.0f)));
        BoxScript box2 = a2.GetComponent<BoxScript>();
        box2.setResult(resp2);

        a3 = Instantiate(caixa, new Vector3(pontoX, 49.89f, -4.7f), Quaternion.Euler(new Vector3(-4.0f,  4.0f, - 0.0f)));
        BoxScript box3 = a3.GetComponent<BoxScript>();
        box3.setResult(resp3);
    }
}
