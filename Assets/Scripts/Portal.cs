using UnityEngine;

public class Portal : Collidable
{
    public string[] sceneNames;

    protected override void OnCollide(Collider2D coll)
    {

        // GameManager.instance.ShowText();

        if(coll.name == "Player")
        {
            // Save game
            GameManager.instance.SaveState();

            // Teleport the player
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];

            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
