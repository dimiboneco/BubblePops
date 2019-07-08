using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Bubble : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Text numberText;
    public int number;
    private int speed = 18;
    public ColorMapper colorMapper;
    private List<Bubble> collisionList = new List<Bubble>();
    public AudioClip blinkCLip;
    public GameObject bubbleExplosion;
    public int scorecounter=0;

    public void Initialize(int number, Color color)
    {
        this.number = number;
        spriteRenderer.color = color;
        numberText.text = number.ToString();
    }

    public void Shoot(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().gravityScale = 0f;
        GetComponent<Rigidbody2D>().AddForce(direction * speed, ForceMode2D.Impulse);
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Bubble>() == null || collision.gameObject.layer !=8)
        {
            return;
        }

        collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        collision.gameObject.GetComponent<Rigidbody2D>().angularVelocity = 0f;
        collision.gameObject.layer = 0;

        if (collision.gameObject.GetComponent<Bubble>().number == number)
        {
            var collidedObject = collision.gameObject.GetComponent<Bubble>();
            var otherCollidedObject = collision.otherCollider.gameObject.GetComponent<Bubble>();
            collisionList.Add(otherCollidedObject);
            collisionList.Add(collidedObject);
            Merge(collisionList);
            collisionList.Clear();
            scorecounter += 5;
            CheckForCeiling(collidedObject);
        }
    }

    public void Merge(List<Bubble> MergeList)
    {
        var targetNumber = CalculatePower(MergeList);
        for (var i = 1; i < MergeList.Count; i++)
        {
            MergeList[i].Destroy();
        }
        MergeList[0].UpdateAfterMerge(targetNumber);
    }

    private int CalculatePower(List<Bubble> bubbles)
    {
        var sum = bubbles[0].number * bubbles.Count;
        var num = 4;
        while (sum >= num)
        {
            num *= 2;
        }

        return num / 2;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void UpdateAfterMerge(int newNumber)
    {
        if (newNumber >= 2048)
        {
            Destroy(this.gameObject);
            Instantiate(bubbleExplosion, transform.position, Quaternion.identity);
        }
        else
        {
            number = newNumber;
            numberText.text = number.ToString();
            spriteRenderer.color = colorMapper.MatchNumberToColor(number);
            AudioSource.PlayClipAtPoint(blinkCLip, Camera.main.transform.position);
        }
    }

    public void CheckForCeiling(Bubble bubble)
    {
        RaycastHit2D ceilingRaycast = Physics2D.Raycast(transform.position, Vector2.up, 1.5f);
        if (ceilingRaycast.collider != null)
        {
            
        }
        else
        {
            Debug.Log("I must fall");
            bubble.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            bubble.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
    }

    public void CheckForNeighbours (Bubble bubble)
    {
        
    }
}
