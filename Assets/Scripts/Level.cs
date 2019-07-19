using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform[] npcPositions;
    public List<NPC> npcList;

    private void OnEnable()
    {
        Invoke("InitLevel", 1f); 
    }

    private void InitLevel()
    {
        StartCoroutine(GenerateNPC());
    }

    private IEnumerator GenerateNPC()
    {
        //create all npc
        for (int i = 0; i < npcPositions.Length; i++)
        {
            NPC anNPC = NPCPool.Instance.Get();
            anNPC.transform.SetParent(npcPositions[i]);
            anNPC.transform.position = npcPositions[i].position;
            anNPC.gameObject.SetActive(true);
            npcList.Add(anNPC);

            GameManager.Instance.audioManager.PlayCreateNPCClip();
            yield return new WaitForSeconds(Random.Range(0.2f, 0.5f));
        }

        yield return new WaitForSeconds(1f);

        GameManager.Instance.SetFirstTargetNPC();
    }

    public NPC PickATargetNPC()
    {
        NPC targetNPC = npcList[Random.Range(0, npcList.Count)];
        targetNPC.SetAsTarget();
        return targetNPC;
    }

    public NPC PickATargetNPC(NPC lastNPC)
    {
        npcList.Remove(lastNPC);

        if (npcList.Count > 0)
        {
            NPC targetNPC = npcList[Random.Range(0, npcList.Count)];
            targetNPC.SetAsTarget();
            return targetNPC;
        }
        else
        {
            return null; 
        }
    }

}
