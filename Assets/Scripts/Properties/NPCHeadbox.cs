using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
public class NPCHeadbox : MonoBehaviour
{
    public NPCProfile npcProfile;
    public TextMesh thisHeadbox;

    private string npcName;
    private string npcProfession;

    // Start is called before the first frame update
    void Start()
    {
        npcName = npcProfile.npcName;
        npcProfession = npcProfile.npcProfession;

        thisHeadbox.text = $"{npcName}\n-{npcProfession}-";
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(transform.position, Camera.main.transform.position);

        Quaternion rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);

        transform.rotation = rotation;
    }
}
