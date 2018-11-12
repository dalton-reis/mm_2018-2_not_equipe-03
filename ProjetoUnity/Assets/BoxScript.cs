using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxScript : MonoBehaviour {
	public GameObject fsZero;
	public GameObject fsOne;
	public GameObject fsTwo;
	public GameObject fsThree;
	public GameObject fsFour;
	public GameObject fsFive;
	public GameObject fsSix;
	public GameObject fsSeven;
	public GameObject fsEight;
	public GameObject fsNine;
	
	public GameObject ssZero;
	public GameObject ssOne;
	public GameObject ssTwo;
	public GameObject ssThree;
	public GameObject ssFour;
	public GameObject ssFive;
	public GameObject ssSix;
	public GameObject ssSeven;
	public GameObject ssEight;
	public GameObject ssNine;

	private int Result;
	void Start () {	
	}

	public int getResult()
	{
		return Result;
	}

	public void setResult(int result)	
	{
		Result = result;
		
		var resultStr = Result.ToString();

		if (resultStr.Length == 1)	{
			resultStr = '0' + resultStr;
		}
		
		switch(resultStr[0]) 
		{
			case '0':
				fsZero.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '1':
				fsOne.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '2':
				fsTwo.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '3':
				fsThree.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '4':
				fsFour.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '5':
				fsFive.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '6':
				fsSix.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '7':
				fsSeven.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '8':
				fsEight.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '9':
				fsNine.GetComponent<MeshRenderer>().enabled = true;	
				break;
		}

		switch(resultStr[1]) 
		{
			case '0':
				ssZero.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '1':
				ssOne.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '2':
				ssTwo.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '3':
				ssThree.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '4':
				ssFour.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '5':
				ssFive.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '6':
				ssSix.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '7':
				ssSeven.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '8':
				ssEight.GetComponent<MeshRenderer>().enabled = true;	
				break;
			case '9':
				ssNine.GetComponent<MeshRenderer>().enabled = true;	
				break;
		}
	}
}
