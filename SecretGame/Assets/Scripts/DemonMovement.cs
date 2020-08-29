using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemonMovement : MonoBehaviour
{
    public Transform target;
    public float maximumMovementSpeed;
    public float currentMovementSpeed;
    public float movementSpeedIncrease;
    Animator animator;
    public int maximumHealth = 100;
    public int currentHealth;
    public GameObject blood;
    public Image healthBar;
    public Image redBar;
    bool dead;
    void Start()
    {
        currentHealth = maximumHealth;
        target = GameManager.Instance.monsterTarget;
        animator = this.GetComponent<Animator>();
        transform.LookAt(target.position);
        Vector3 rotation = this.transform.rotation.eulerAngles;
        rotation.x = 0;
        this.transform.rotation = Quaternion.Euler(rotation);
    }

    void Update()
    {
        if (dead) return;
        if (currentMovementSpeed < 1f)
        {
            currentMovementSpeed += movementSpeedIncrease;
            currentMovementSpeed = Mathf.Clamp(currentMovementSpeed, 0f, 1f);
        }
        animator.SetFloat("MovementSpeed", currentMovementSpeed);
        if (currentMovementSpeed > 0.5f)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, currentMovementSpeed * maximumMovementSpeed * Time.deltaTime);
        }
    }

    public void HitReaction(int damage, Vector3 hitPosition)
    {
        if (dead) return;
        currentHealth -= damage;
        healthBar.fillAmount = (float)currentHealth / (float)maximumHealth;
        GameObject bloodGO = Instantiate(blood, hitPosition, Quaternion.identity);
        Destroy(bloodGO, 1f);
        currentMovementSpeed = 0;
        if (currentHealth <= 0)
        {
            Die();
            return;
        }

            animator.SetTrigger("Hit");
        //Vector3 bloodRotation = Vector3.Angle(this.transform.forward, this.transform.position - hitPosition)

    }

    public void Die()
    {
        dead = true;
        redBar.gameObject.SetActive(false);
        this.GetComponent<CapsuleCollider>().enabled = false;
        animator.SetTrigger("Die");
        GameManager.Instance.UpdateScore(10);
        Destroy(this.gameObject, 2f);
    }
}
