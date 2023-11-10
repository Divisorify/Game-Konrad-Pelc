using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void PlayGame() {
        int selectedCharacter = 
            int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);

        GameManager.instance.CharIndex = selectedCharacter;

        //Car car = new Car();

        //car.name = "Car";
        //car.Speed = 50;
        //car.Health = 100;

        //Warrior.name = "Warrior";
        //Warrior.power = 100; ,bo static dodany


        //Debug.Log("Index: " + clickedObj);
        
        SceneManager.LoadScene("Gameplay");
    }
}
