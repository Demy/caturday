using UnityEngine;

public class InstagramKeyboardController : MonoBehaviour
{
    private InstagramMinigame game;

    void Start()
    {
        game = GetComponent<InstagramMinigame>();
    }

    void Update()
    {
        if (game.IsNotStopped())
        {
            if (Input.GetKeyDown(KeyCode.Q)) game.MakePose("Q");
            if (Input.GetKeyDown(KeyCode.W)) game.MakePose("W");
            if (Input.GetKeyDown(KeyCode.E)) game.MakePose("E");
            if (Input.GetKeyDown(KeyCode.A)) game.MakePose("A");
            if (Input.GetKeyDown(KeyCode.S)) game.MakePose("S");
            if (Input.GetKeyDown(KeyCode.D)) game.MakePose("D");
        }
    }
}