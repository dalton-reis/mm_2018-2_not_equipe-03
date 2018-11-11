using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeradorScript : MonoBehaviour {

    public GameObject[] listaPrefab;
    public GameObject caixa;
    public Transform player;
    public Camera cam;
    Vector3 velocity;
    
    float pontoZ = 106.0f;
    List<GameObject> listaAtivos;
	// Use this for initialization
	void Start () {
        velocity = new Vector3(20.0f, 0.0f, 0.0f);
        listaAtivos = new List<GameObject>();
    }
	
	// Update is called once per frame
	void Update () {
        cam.transform.position += velocity * Time.fixedDeltaTime;
        player.transform.position += velocity * Time.fixedDeltaTime;

        if ((pontoZ - 106.0f) - cam.transform.position.x < (120.8f * 2))
        {
            GameObject obj = Instantiate(listaPrefab[0], new Vector3(pontoZ, 50.0f, 0.0f), Quaternion.LookRotation(new Vector3(0.0f, 90.0f, 0.0f)));
            pontoZ += 120.8f;

            GameObject caixaTemp = Instantiate(caixa, new Vector3(pontoZ, 50.0f, 0.0f), Quaternion.LookRotation(new Vector3(0.0f, 90.0f, 0.0f)));

            listaAtivos.Add(obj);
            if (listaAtivos.Count > 3) {
                GameObject objDestroy = listaAtivos[0];
                listaAtivos.Remove(objDestroy);
                Destroy(objDestroy);
            }

            velocity.x += 1.0f;
        }
    }
}
