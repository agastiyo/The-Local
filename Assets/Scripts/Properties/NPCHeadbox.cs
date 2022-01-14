using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TextMesh))]
[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class NPCHeadbox : MonoBehaviour
{
    public NPCProfile npcProfile;
    public TextMesh thisHeadboxText;
    public BoxCollider thisHeadboxCollider;
    public MeshRenderer thisHeadboxRenderer;

    // Start is called before the first frame update
    void Start()
    {
        thisHeadboxRenderer.enabled = false;
        thisHeadboxText.text = $"{npcProfile.npcName}\n-{npcProfile.npcProfession}-";
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
        transform.rotation = rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        thisHeadboxRenderer.enabled = true;
    }

    private void OnTriggerExit(Collider other)
    {
        thisHeadboxRenderer.enabled = false;
    }
}
