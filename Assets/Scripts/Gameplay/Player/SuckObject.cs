using PinkBlob.Gameplay.Ability;
using Sirenix.OdinInspector;
using Unity.VisualScripting;
using UnityEngine;

namespace PinkBlob.Gameplay.Player
{
    public class SuckObject : MonoBehaviour
    {
        [Title("Properties")]

        [Min(0)]
        [SerializeField]
        private float movementSpeed = 50f;

        [Min(0)]
        [SerializeField]
        private int damage = 1;
        
        [Min(0)]
        [SerializeField]
        private float sphereCheckSize = 0.5f;

        [SerializeField]
        private LayerMask layerMask = default;

        [Title("Ability")]

        [SerializeField]
        private AbilityType abilityType = AbilityType.Normal;

        public AbilityType AbilityType => abilityType;

        private bool hasHit;

        private void Update()
        {
            if (!hasHit)
            {
                Transform trans = transform;

                Vector3 pos = trans.position;
                float move = movementSpeed * Time.deltaTime;

                var ray = new Ray(pos, trans.forward);
                if (Physics.SphereCast(ray, sphereCheckSize, out RaycastHit hit, move, layerMask))
                {
                    if (hit.collider.TryGetComponent(out Health health))
                    {
                        health.DealDamage(damage);
                        hasHit = true;
                        transform.position = hit.point;
                        return;
                    }
                }

                pos += move * trans.forward;
                transform.position = pos;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
