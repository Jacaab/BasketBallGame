using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterPreviewScript : MonoBehaviour
{
    public GameObject character1Preview;
    public GameObject character2Preview;

    public void player1SelectionChange()
    {
        if (settingsScript.player1CharacterSelect == 1)
        {
            character1Preview.transform.localScale = new Vector3(1, 1.6f, 1);
        }
        else if (settingsScript.player1CharacterSelect == 2)
        {
            character1Preview.transform.localScale = new Vector3(1, 1.8f, 1);
        }
        else if (settingsScript.player1CharacterSelect == 3)
        {
            character1Preview.transform.localScale = new Vector3(1, 1.4f, 1);
        }
    }

    public void player2SelectionChange()
    {
        if (settingsScript.player2CharacterSelect == 1)
        {
            character2Preview.transform.localScale = new Vector3(1, 1.6f, 1);
        }
        else if (settingsScript.player2CharacterSelect == 2)
        {
            character2Preview.transform.localScale = new Vector3(1, 1.8f, 1);
        }
        else if (settingsScript.player2CharacterSelect == 3)
        {
            character2Preview.transform.localScale = new Vector3(1, 1.4f, 1);
        }
    }

}
