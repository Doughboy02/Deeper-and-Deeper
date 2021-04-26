using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEnemy : MonoBehaviour
{
    public SpriteRenderer Sprite;
    public MeshRenderer RangeRenderer;
    public SphereCollider RangeCollider;
    public EnemyType Type;
    private bool IsAnimating = false;

    public void SetOutOfView()
    {

        RangeCollider.enabled = false;
        StartCoroutine(AnimateSpriteDown(.05f));
    }

    public void SetInView()
    {
        Sprite.enabled = true;
        RangeRenderer.enabled = true;
        RangeCollider.enabled = true;
        StartCoroutine(AnimateSpriteUp(.05f));
    }

    private IEnumerator AnimateSpriteDown(float animtionTime)
    {
        yield return new WaitUntil(() => !IsAnimating);

        int smoothness = 30;
        float position;
        IsAnimating = true;
        for (int i = 0; i < smoothness; i++)
        {
            position = Vector3.Lerp(transform.position, transform.up * -3, (float)i / smoothness).y;
            Sprite.material.SetVector("_HeightOfBlock", new Vector4(0, position, 0));
            RangeRenderer.material.SetVector("_HeightOfBlock", new Vector4(0, position, 0));
            yield return new WaitForSeconds(animtionTime / smoothness);
        }

        Sprite.material.SetVector("_HeightOfBlock", new Vector4(0, (transform.up * -3).y, 0));
        RangeRenderer.material.SetVector("_HeightOfBlock", new Vector4(0, (transform.up * -3).y, 0));

        Sprite.enabled = false;
        RangeRenderer.enabled = false;
        IsAnimating = false;
    }

    private IEnumerator AnimateSpriteUp(float animtionTime)
    {
        yield return new WaitUntil(() => !IsAnimating);

        int smoothness = 30;
        float position;
        IsAnimating = true;
        for (int i = 0; i < smoothness; i++)
        {
            position = Vector3.Lerp(transform.up * -3, transform.position, (float)i / smoothness).y;

            Sprite.material.SetVector("_HeightOfBlock", new Vector4(0, position, 0));
            RangeRenderer.material.SetVector("_HeightOfBlock", new Vector4(0, position, 0));

            yield return new WaitForSeconds(animtionTime / smoothness);
        }

        Sprite.material.SetVector("_HeightOfBlock", new Vector4(0, 0, 0));
        RangeRenderer.material.SetVector("_HeightOfBlock", new Vector4(0, 0, 0));

        IsAnimating = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!Sprite.enabled && !collision.gameObject.CompareTag("Player")) SetInView();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (Sprite.enabled && !collision.gameObject.CompareTag("Player")) SetOutOfView();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!Sprite.enabled && !collision.gameObject.CompareTag("Player")) SetInView();
    }
}

public enum EnemyType
{ 
    MurderShroom,
    Oozer
}

