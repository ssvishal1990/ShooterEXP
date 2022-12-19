using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abiilities : Player
{
    protected Player player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void Initialize()
    {
        base.Initialize();
        player = GetComponent<Player>();
    }


}
