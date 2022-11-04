using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;
using System.Collections;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using Newtonsoft.Json;

public class PokemonAPI : MonoBehaviour
{
    //public Cards[] card;     

    [SerializeField] private TextMeshProUGUI itemNameUI;
    [SerializeField] private TextMeshProUGUI itemHpUI;
    [SerializeField] private TextMeshProUGUI itemRarityUI;
    //[SerializeField] private TextMeshProUGUI itemTypeUI;
    [SerializeField] private RawImage itemIconUI;


    private const string URL = "https://api.pokemontcg.io/v2/cards";
    //private const string URLType = "https://api.pokemontcg.io/v2/types";
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

                int randomNum = Random.Range(1, itemsData["data"].Count);

                //Debug.Log("The generated item is: \nName: " + itemsData["data"][randomNum]["name"]);



                string itemName = itemsData["data"][randomNum]["name"];
                //string itemType = itemsData["data"][randomNum]["types"];
                string itemHp = itemsData["data"][randomNum]["hp"];
                string itemRarity = itemsData["data"][randomNum]["rarity"];
                string itemImageUrl = itemsData["data"][randomNum]["images"]["large"];

                UnityWebRequest itemImageRequest = UnityWebRequestTexture.GetTexture(itemImageUrl);

                yield return itemImageRequest.SendWebRequest();

                if (itemImageRequest.isNetworkError || itemImageRequest.isHttpError)
                {
                    Debug.LogError(itemImageRequest.error);
                    yield break;
                }

                itemNameUI.text = "Name: " + itemName;
                //itemTypeUI.text = "Type: " + itemType;
                itemHpUI.text = "HP: " + itemHp;
                itemRarityUI.text = "Rarity: " + itemRarity;
                itemIconUI.texture = DownloadHandlerTexture.GetContent(itemImageRequest);
                itemIconUI.texture.filterMode = FilterMode.Point;

                //TextMeshProUGUI nameUI = GetComponent<Cards>().itemNameUI;
                ////TextMeshProUGUI TypeUI = GetComponentInChildren<Cards>().itemNameUI;
                //TextMeshProUGUI HpUI = GetComponent<Cards>().itemNameUI;
                //TextMeshProUGUI RarityUI = GetComponent<Cards>().itemNameUI;
                //RawImage IconUI = GetComponent<Cards>().itemIconUI;

                //nameUI.text = "Name: " + itemName;
                ////TypeUI.text = "Type: " + itemType;
                //HpUI.text = "HP: " + itemHp;
                //RarityUI.text = "Rarity: " + itemRarity;
                //IconUI.texture = DownloadHandlerTexture.GetContent(itemImageRequest);
                //IconUI.texture.filterMode = FilterMode.Point;


            }
        }
    }
}
