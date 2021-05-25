using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class settingsScript : MonoBehaviour
{
    public static int player1CharacterSelect = 1;
    public static int player2CharacterSelect = 1;

    public AudioMixer audioMixer;

    public void player1IncreaseSelection() 
    {
        player1CharacterSelect++;
        if (player1CharacterSelect > 3)
        { 
            player1CharacterSelect = 1; 
        }
    }
    public void player1DecreaseSelection()
    {
        player1CharacterSelect--;
        if (player1CharacterSelect < 1)
        {
            player1CharacterSelect = 3;
        }
    }

    public void player2IncreaseSelection()
    {
        player2CharacterSelect++;
        if (player2CharacterSelect > 3)
        {
            player2CharacterSelect = 1;
        }
    }
    public void player2DecreaseSelection()
    {
        player2CharacterSelect--;
        if (player2CharacterSelect < 1)
        {
            player2CharacterSelect = 3;
        }
    }

    public void setVolume(float _input)
    {
        audioMixer.SetFloat("volume", _input);
    }

}
