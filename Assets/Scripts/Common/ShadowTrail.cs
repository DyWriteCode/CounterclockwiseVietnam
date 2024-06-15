using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShadowTrail : MonoBehaviour
{
    public SpriteRenderer master;
    public int lifeTime;
    public float interval;
    public float duration;
    public Gradient colorGradient;
    public GameObject shadowTemp;
    public List<SpriteRenderer> shadows;
    float _objLifeTime;
    float _timePassed;

    void Start()
    {
        if(master == null)
        {
            master = GetComponentInParent<SpriteRenderer>();
            transform.parent = null;
        }
        if(master != null)
        {
            _objLifeTime = lifeTime + duration;
            StartCoroutine(ShadowGenerator());
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator ShadowGenerator()
    {
        while(_objLifeTime > duration)
        {
            if(_timePassed >= interval)
            {
                var obj = Instantiate(shadowTemp, gameObject.transform);
                obj.transform.position = master.transform.position;
                obj.transform.rotation = master.transform.rotation;
                obj.transform.localScale = master.transform.lossyScale;
                var sr = obj.GetComponent<SpriteRenderer>();
                sr.sprite = master.sprite;
                shadows.Add(sr);
                StartCoroutine(OneShadowFade(sr));  
                _timePassed = 0;
            }
            yield return null;
            _timePassed += Time.deltaTime;
            _objLifeTime -= Time.deltaTime;
        }
            yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
    IEnumerator OneShadowFade(SpriteRenderer sr)
    {
        sr.enabled = true;
        float startTime = Time.time;
        while(Time.time - startTime  < duration)
        {
            var timePassed = Time.time - startTime;
            var per = timePassed / duration;
            sr.color = colorGradient.Evaluate(per);
            yield return new WaitForSeconds(0.03f);
        }
        Destroy(sr.gameObject);
    }
}
