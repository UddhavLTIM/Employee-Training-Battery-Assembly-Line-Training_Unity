using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class playSteps : MonoBehaviour
{
    public List<Step> steps;

    protected PlayableDirector m_director;

    [System.Serializable]
    public class Step
    {
        public string name;
        public float time;
        public bool hasPlayed;
    }

    public void PlayStepIndex(int index)
    {
        Step step = steps[index];

        if (!step.hasPlayed)
        {
            step.hasPlayed = true;

            m_director.Stop();
            m_director.time = step.time;
            m_director.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_director = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
