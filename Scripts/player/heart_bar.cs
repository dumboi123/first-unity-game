using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class heart_bar : MonoBehaviour
{
    [SerializeField] private GameObject heartprefab;
    [SerializeField] private Life PlayerLife;
    public bool onenables =true;
    List<hearts> HeathHeart = new List<hearts>();
    // Start is called before the first frame update
    private void OnEnable()
    {
        Life.OnPlayerLife += DrawHearts;
    }
    private void OnDisable()
    {
        Life.OnPlayerLife -= DrawHearts;
    }
   // need to check again enable,disable mean
    private void Start()
    {
        DrawHearts();
    }
    private void DrawHearts()
    {
        clearhearts();
        float MaxHealthRemainder = PlayerLife.maxhealth % 2;
        byte MakeHearts = (byte)((PlayerLife.maxhealth / 2) + MaxHealthRemainder);
        for(byte i = 0; i< MakeHearts; i++)
            createEmptyHearts();
        for(byte  i=0;  i< HeathHeart.Count; i++)
        {
            byte heartStatusRemainder = (byte)Mathf.Clamp(PlayerLife.health - (i * 2), 0, 2);
            HeathHeart[i].displayHeart((hearts.Heartstatus)heartStatusRemainder);
        }

    }
    private void createEmptyHearts()
    {
        //
        GameObject newHeart = Instantiate(heartprefab);
        newHeart.transform.SetParent(transform);

        hearts heartComponent = newHeart.GetComponent<hearts>();
        heartComponent.displayHeart(hearts.Heartstatus.Empty);

        HeathHeart.Add(heartComponent);
    }
    private void clearhearts()
    {
        foreach (Transform t in transform)
            Destroy(t.gameObject);
        HeathHeart = new List<hearts>();
    }
}
