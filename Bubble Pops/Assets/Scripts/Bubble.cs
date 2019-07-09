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
    private UIHandler uihandler;

    public void Initialize(int number, Color color, UIHandler uihandler)
    {
        this.uihandler = uihandler;
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
        if(collision.gameObject.GetComponent<Bubble>() == null || gameObject.layer !=8)
        {
            return;
        }

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GetComponent<Rigidbody2D>().angularVelocity = 0f;
        gameObject.layer = 0;

        var neighbours= collision.gameObject.GetComponent<Bubble>().GetNeighbours().Where(c=>c.number == number);
        var otherCollidedObject = collision.otherCollider.gameObject.GetComponent<Bubble>();
        neighbours.ToList().Add(otherCollidedObject);

        if (neighbours.Count() >1)
        {
            Merge(neighbours.ToList());
            uihandler.Score(neighbours.Count());
        }
    }

    public void Merge(List<Bubble> MergeList)
    {
        var targetNumber = CalculatePower(MergeList);
        for (var i = 0; i < MergeList.Count-1; i++)
        {
            MergeList[i].Destroy();
        }
        MergeList[MergeList.Count - 1].UpdateAfterMerge(targetNumber);
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
            Instantiate(bubbleExplosion, transform.position, Quaternion.identity);
            var neighbourstodestroy = GetNeighbours();
            foreach (var n in neighbourstodestroy)
            {
                Destroy(n.transform.gameObject);
            }
            Destroy(this.gameObject);
        }
        else
        {
            number = newNumber;
            numberText.text = number.ToString();
            spriteRenderer.color = colorMapper.MatchNumberToColor(number);
            AudioSource.PlayClipAtPoint(blinkCLip, Camera.main.transform.position);
        }
    }

    public List<Bubble> GetNeighbours ()
    {
        var neighbourList = new List<Bubble>();
        var colliders = Physics2D.OverlapCircleAll(transform.position, 0.8f);
        return colliders.Where(c => c.GetComponent<Bubble>() != null).Select(c => c.GetComponent<Bubble>()).ToList();
    }
}
