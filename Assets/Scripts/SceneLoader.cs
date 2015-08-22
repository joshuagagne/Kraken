using UnityEngine;
using System.Collections;

public class SceneLoader : MonoBehaviour
{

	public void LoadLevel ()
	{
		Debug.Log ("Load");
		Application.LoadLevel ("kraken");
	}
}
