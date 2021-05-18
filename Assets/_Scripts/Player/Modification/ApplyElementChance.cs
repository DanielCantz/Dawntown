using UnityEngine;
///
/// Author: Samuel Müller: sm184, Daniel Cantz dc029, Yannick Pfeifer yp009
/// Description: Applies Element debuffs to colliding buffhandlers based on chance.
/// ==============================================
/// Changelog: 
/// ==============================================
///
public class ApplyElementChance : MonoBehaviour
{
    private Element _currentElement;
    private float _damage;
    private float _lifedrain;

    public Element CurrentElement { get => _currentElement; set => _currentElement = value; }
    public float Damage { get => _damage; set => _damage = value; }
    public float Lifedrain { get => _lifedrain; set => _lifedrain = value; }

    void Start()
    {
        Physics.IgnoreLayerCollision(10, 10); //Ignore PLayer
    }

    private void OnCollisionEnter(Collision collision)
    {

        OnTriggerEnter(collision.collider);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GameObject enemyGameObject = collision.gameObject;
            ApplyDebuff(enemyGameObject);
        }
    }
    private void ApplyDebuff(GameObject gameObject)
    {
        StatHandler enemyStatHandler = gameObject.GetComponent<StatHandler>();
        BuffHandler enemyBuffHandler = gameObject.GetComponent<BuffHandler>();
        float randomNumber = Random.Range(0.0f, 1.0f);
        if (randomNumber < _currentElement.Chance)
        {
            switch (_currentElement.ElementEnum)
            {
                case ElementEnum.fire:
                    Burn burn = new Burn(enemyStatHandler, new Stat(_currentElement.Duration), _currentElement.Value);
                    enemyBuffHandler.AddBuff(burn);
                    break;
                case ElementEnum.ice:
                    Slow slow = new Slow(enemyStatHandler, new Stat(_currentElement.Duration), new ScalingStatModificator(_currentElement.Value));
                    enemyBuffHandler.AddBuff(slow);
                    break;
                case ElementEnum.lightning:
                    Stun stun = new Stun(enemyStatHandler, new Stat(_currentElement.Duration));
                    enemyBuffHandler.AddBuff(stun);
                    break;
                case ElementEnum.neutral:
                    break;
            }
        }

        if (enemyStatHandler)
        {
            enemyStatHandler.TakeDamage(_damage);
            GameObject.FindGameObjectWithTag("Player").GetComponent<StatHandler>().Heal(_damage * _lifedrain);
        }
    }

}
