using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorScript : MonoBehaviour
{
    [SerializeField]
    float speed;
    Material converyorBelt;

    Rigidbody rBody;
    // Start is called before the first frame update
    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        converyorBelt = GetComponent<MeshRenderer>().material;
    }

    private void Update()
    {
        TextureAnimation();   
    }

    private void TextureAnimation()
    {
        converyorBelt.mainTextureOffset += new Vector2(0, 1) * speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        FixedConveyorMove();
    }

    private void FixedConveyorMove()
    {
        Vector3 pos = rBody.position;
        rBody.position += Vector3.forward * speed * Time.fixedDeltaTime;
        rBody.MovePosition(pos);
    }
}
