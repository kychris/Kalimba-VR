using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR;

using System.Collections.Generic;



public class TouchKey : MonoBehaviour
{
    //Public Access
    public Material SkyMaterial;
    public string side;

    //key input listening
    static List<string> keys = new List<string>();

    //timelines
    PlayableDirector timeline;
    PlayableDirector timeline1;
    PlayableDirector timeline2;
    PlayableDirector timeline3;
    PlayableDirector timeline4;

    //Scene objects
    LightController pl1;
    LightController pl2;
    LightController pl3;
    LightController pl4;
    PenguinController penguin;
    FoxController fox;
    SunController sun;
    FogController fog;
    NVBoids boid;
    

    #region Notes definition
    //music notes
    List<string> beginning1 = new List<string>{"C4", "E4", "G4", "B4"};
    List<string> beginning2 = new List<string>{"D4", "F4", "A4", "C5"};
    List<string> beginning3 = new List<string>{"E4", "G4", "B4", "D5"};
    List<string> beginning4 = new List<string>{"A4", "D5", "F5", "E5"};
    List<string> timeLine1Notes = new List<string>{"F4", "E4", "F4", "C4"};
    List<string> timeLine2Notes = new List<string>{"F4", "E4", "F4", "G4"};
    List<string> timeLine3Notes = new List<string>{"B4", "C5", "D5", "E5"};
    // List<string> timeLine4Notes = new List<string>{"G5", "D5", "E5", "B4"};
    List<string> timeLine4Notes = new List<string>{"E5", "G5", "G5", "G5"};
    List<string> epic = new List<string>{"C5", "B4", "G4", "E4"};
    #endregion

    void Start() 
    {
        SkyMaterial.SetFloat("_Blend", 1);
        RenderSettings.fogDensity = 1;
        
        timeline = GameObject.Find("Timeline-Start").GetComponent<PlayableDirector>();
        timeline3 = GameObject.Find("Timeline3").GetComponent<PlayableDirector>();
        timeline4 = GameObject.Find("Timeline4").GetComponent<PlayableDirector>();

        pl1 = GameObject.Find("PL1").GetComponent<LightController>();
        pl2 = GameObject.Find("PL2").GetComponent<LightController>();
        pl3 = GameObject.Find("PL3").GetComponent<LightController>();
        pl4 = GameObject.Find("PL4").GetComponent<LightController>();

        penguin = GameObject.Find("Penguin").GetComponent<PenguinController>();
        sun = GameObject.Find("Directional Light").GetComponent<SunController>();
        fox = GameObject.Find("Fox").GetComponent<FoxController>();
        fog = GameObject.Find("Volumetric Fog Area Sphere").GetComponent<FogController>();
        boid = GameObject.Find("NVBoids Bird").GetComponent<NVBoids>();
        boid.enabled = false;

        // playTimeLine2();
        
    }

    // Update is called once per frame
    void Update() {
    }

    void PlaySkyChange() { StartCoroutine("SkyChange"); }
    IEnumerator SkyChange() 
    {
        float BlendVal = 1;
        float tParam = 0.0f;
        float speed = 0.3f;

        while (tParam < 1) 
        {
            tParam += Time.deltaTime * speed; //This will increment tParam based on Time.deltaTime multiplied by a speed multiplier
            BlendVal = Mathf.Lerp(1, 0, tParam);
            RenderSettings.fogDensity = BlendVal;
            SkyMaterial.SetFloat("_Blend", BlendVal);
            yield return null;
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag == "key")
        {
            //If the GameObject has the same tag as specified, output this message in the console
            foreach (ContactPoint contact in collision.contacts)
            {
                if (contact.normal.y == 1 | contact.normal.z >= 0.9) //only play if contact from top
                {   
                    keys.Add(collision.gameObject.name);

                    AudioSource audioData = collision.gameObject.GetComponent<AudioSource>();
                    audioData.Play(0);

                    checkPattern();
                    vibrate();
                    break;
                }
                
            }
            
        }
    }

    void vibrate() 
    {
        InputDevice device;
        if (side == "Left") {
            device = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        } else {
            device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        }
        
        HapticCapabilities capabilities;
        if(device.TryGetHapticCapabilities(out capabilities))
        {
            if (capabilities.supportsImpulse)
            {
                uint channel = 0;
                float amplitude = 0.3f;
                float duration = 0.1f;
                device.SendHapticImpulse(channel, amplitude, duration);
            }
        }
    }
    
    void checkPattern() 
    {
        if (keys.Count >= 4) 
        {
            print_arr(keys);
            List<string> lastNotes = keys.GetRange(keys.Count - 4, 4);
            
            if (Equal(beginning1, lastNotes)) { pl3.StartFadeIn(); }
            if (Equal(beginning2, lastNotes)) { pl2.StartFadeIn(); }
            if (Equal(beginning3, lastNotes)) { pl4.StartFadeIn(); }
            if (Equal(beginning4, lastNotes)) { pl1.StartFadeIn(); }
            if (Equal(timeLine1Notes, lastNotes)) { playTimeLine1(); }
            if (Equal(timeLine2Notes, lastNotes)) { playTimeLine2(); }
            if (Equal(timeLine3Notes, lastNotes)) { playTimeLine3(); }
            if (Equal(timeLine4Notes, lastNotes)) { playTimeLine4(); }
            if (Equal(epic, lastNotes)) { PlaySkyChange(); }
        }
    }

    void playTimeLine1() { penguin.StartMoveIn(); }

    void playTimeLine2() { sun.StartMinorRise(); fox.StartMoveIn(); }

    void playTimeLine3() { timeline3.Play(); }

    void playTimeLine4() {
        fox.StartTurn();
        penguin.StartTurn();
        fog.StartFadeOut();
        sun.StartFullRise();
        timeline4.Play();
        boid.enabled = true;
    }

    bool Equal(List<string> l1, List<string> l2) //check if two list are equal
    {
        if (l1.Count != l2.Count)
            return false;

        for (int i = 0; i < l1.Count; i++) 
        {
            if (l1[i] != l2[i])
                return false;
        }
        return true;
    }

    void print_arr(List<string> l) //print out input list to terminal
    {
        string total = "";
        foreach(var x in l) {
            total += x.ToString() + " ";
        }
        print(total);
    }
}
