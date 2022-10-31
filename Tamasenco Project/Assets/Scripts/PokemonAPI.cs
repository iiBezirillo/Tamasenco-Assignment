using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using SimpleJSON;
public class PokemonAPI : MonoBehaviour
{
    private const string URL = "https://api.pokemontcg.io/v2/cards";
    //private const string HOST = "";
    private const string API_KEY = "ffcdaab5-1147-4617-95e2-a37f37254a9a";

    public void GenerateRequest()
    {
        StartCoroutine(ProcessRequest(URL));
    }

    private IEnumerator ProcessRequest(string uri)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(uri))
        {
            request.SetRequestHeader("X-Api-Key", API_KEY);

            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.Log(request.error);
            }
            else
            {
                //Debug.Log(request.downloadHandler.text);

                JSONNode itemsData = JSON.Parse(request.downloadHandler.text);

                int randomNum = Random.Range(1, itemsData["items"]["backpack"].Count);

                Debug.Log("The generated item is: \nName: " + itemsData["name"]["hp"]["rarity"]);
            }
        }
    }
}