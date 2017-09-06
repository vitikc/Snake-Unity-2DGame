using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodGenerator : MonoBehaviour {
    public GameObject Food;
    public bool isNeedToGenerate = true;
    public Vector2 generatedFoodPosition;
    public GameObject generatedFood;
	
    void GenerateFood()
    {
        int x = Random.Range(Constants.OffsetWidth, Constants.Width+Constants.OffsetWidth);
        int y = Random.Range(Constants.OffsetHeigth, Constants.OffsetHeigth-Constants.Heigth);
        Vector3 position = new Vector3(x, y);
        generatedFoodPosition.x = x;
        generatedFoodPosition.y = y;
        generatedFood = Instantiate(Food, position, Quaternion.identity, this.transform);
    }


	// Update is called once per frame
	void Update () {
        if (isNeedToGenerate)
        {
            GenerateFood();
            isNeedToGenerate = false;
        }
	}
}