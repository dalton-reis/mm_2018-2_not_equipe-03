using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorScript : MonoBehaviour {
	public GeradorScript gerador;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter(Collision other) 
    {
		if (other.gameObject.tag == "Caixa")	{
			Debug.Log(other.gameObject.tag);
			gerador.validarResultado(other.gameObject.GetComponent<BoxScript>().getResult());
			Destroy(other.gameObject);
		}
	}
}
