using System.Collections;
using System.Collections.Generic;
using extOSC;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerOSC : MonoBehaviour
{
    public extOSC.OSCReceiver receiver;
    public extOSC.OSCTransmitter transmitter;

    public Player player;

    private Rigidbody2D rb;

    void TraiterOscRouler(extOSC.OSCMessage message)
    {
        if (message.Values.Count == 0)
        {
            Debug.Log("No value in OSC message");
            return;
        }

        if (message.Values[0].Type != OSCValueType.Int)
        {
            Debug.Log("Value in message is not an Int");
            return;
        }

        // Récupérer la valeur de l’angle depuis le message OSC
        int value = message.Values[0].IntValue;

        rb.AddTorque(value * -40f); // clockwise
    }

    int previousButtonValue = 1;

    void TraiterOscSauter(extOSC.OSCMessage message)
    {
        if (message.Values.Count == 0)
        {
            Debug.Log("No value in OSC message");
            return;
        }

        if (message.Values[0].Type != OSCValueType.Int)
        {
            Debug.Log("Value in message is not an Int");
            return;
        }

        // Récupérer la valeur de l’angle depuis le message OSC
        int value = message.Values[0].IntValue;

        if (value != previousButtonValue)
        {
            previousButtonValue = value;
            if (player.IsGrounded() && value == 0)
            {
                rb.AddForce(Vector2.up * player.jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        receiver.Bind("/encodeur/changement", TraiterOscRouler);
        receiver.Bind("/encodeur/bouton", TraiterOscSauter);
        rb = player.gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() { }
}
