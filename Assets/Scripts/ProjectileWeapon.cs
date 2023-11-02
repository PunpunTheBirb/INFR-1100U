using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileWeapon : MonoBehaviour
{

 [Header("Weapon Base Stats")]
 [SerializeField] protected float timeBetweenAttacks;
 [SerializeField] protected float chargeUpTime;
 [SerializeField, Range(0,1)] protected float minChargePercent;
 [SerializeField] private bool isFullAuto;
 
 private Coroutine _currentFireTimer;
 private bool _isOnCooldown;
 private float _currentChargeTime;

 private WaitForSeconds _coolDownWait;
 private WaitUntil _coolDownEnforce;

 private void Start()
 {
    _coolDownWait = new WaitForSeconds(timeBetweenAttacks);
    _coolDownEnforce = new WaitUntil(() => _isOnCooldown);
 }

 public void StartShooting()
 {
    _currentFireTimer = StartCoroutine(ReFireTimer());
 }

 public void StopShooting()
 {
    StopCoroutine(_currentFireTimer);

    float percent = _currentChargeTime / chargeUpTime;

    if (pecent != 0) TryAttack(percent);
 }

 protected abstract void Attack();

 private IEnumerator CooldownTimer()
 {
    _isOnCooldown = true;
    yield return (_coolDownWait);
    _isOnCooldown = false;
 }

 private IEnumerator ReFireTimer()
 {
    print("Wating for Cooldown");
    yield return  _coolDownEnforce;
    print("Post Cooldown");

    while (_currentChargeTime < chargeUpTime)
    {
        _currentChargeTime += Time.deltaTime;
        yield return null;
    }

    TryAttack(1);
    yield return null;
 }

 private void TryAttack(float percent)
 {
    _currentChargeTime = 0;
    if (!CanAttack(percent)) return;

    Attack(percent);

    StartCoroutine(CooldownTimer());

    if (isFullAuto) _currentFireTimer = StartCoroutine(ReFireTimer());


 }

 protected virtual bool CanAttack(float percent)
 {
    return !_isOnCooldown && percent >= minChargePercent;
 }

 protected abstract void Attack(float percent);
}
