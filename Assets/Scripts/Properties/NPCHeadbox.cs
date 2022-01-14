using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class NPCHeadbox : MonoBehaviour
{
    public NPCProfile npcProfile;
    public TextMesh thisHeadbox;

    // Start is called before the first frame update
    void Start()
    {
        thisHeadbox.text = $"{npcProfile.npcName}\n-{npcProfile.npcProfession}-";
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        transform.rotation = rotation;
    }
}
