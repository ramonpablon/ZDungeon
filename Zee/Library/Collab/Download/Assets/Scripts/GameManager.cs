using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField]
    public static GameObject limitsColliderL, limitsColliderR;
    public GameObject limitsCollL, limitsCollR;

    [Space]
    public GameObject P1, P2;
    public float P1_Life, P2_Life;

	// Use this for initialization
	void Awake () {
        limitsColliderL = limitsCollL;
        limitsColliderR = limitsCollR;

        P1.GetComponent<PlayerPhisics>().life = P1_Life;
        P2.GetComponent<PlayerPhisics>().life = P2_Life;
    }

    // Update is called once per frame
    void Update () {
        P1.GetComponent<PlayerPhisics>().rend.material.SetFloat("Vector1_93698D3F", 5);

        foreach (Transform childs in P1.transform.GetChild(0))
        {
            childs.GetComponent<Renderer>().material.SetFloat("Vector1_93698D3F", 5);
        }

        //print(P1.GetComponent<PlayerPhisics>().life + " "+ P2_Life);
    }
}
