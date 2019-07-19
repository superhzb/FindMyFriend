using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCPool : GenericObjectPool<NPC>
{
    public Texture[] bodies;
    public Texture[] faces;
    public Texture[] hairs;
    public Texture[] kits;

    protected override void Init(NPC npc)
    {
        Texture body = bodies[Random.Range(0, bodies.Length)];
        Texture face = faces[Random.Range(0, faces.Length)];
        Texture hair = hairs[Random.Range(0, hairs.Length)];
        Texture kit = kits[Random.Range(0, kits.Length)];

        npc.InitNPC(body, face, hair, kit);
    }


}
