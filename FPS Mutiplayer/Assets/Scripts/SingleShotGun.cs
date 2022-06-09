using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleShotGun : Gun
{
    [SerializeField] Camera cam;

    PhotonView pv;

    private void Awake()
    {
        pv = GetComponent<PhotonView>();
    }

    public override void Use()
    {
        Shoot();
    }

    void Shoot()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        ray.origin = cam.transform.position;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.GetComponent<IDamageable>() != null)
            {
                if (hit.point.y >= hit.collider.gameObject.transform.position.y + 0.5f)
                {
                    hit.collider.gameObject.GetComponent<IDamageable>().TakeDamage(((GunInfo)itemInfo).damage*3);
                }
                else
                {
                    hit.collider.gameObject.GetComponent<IDamageable>().TakeDamage(((GunInfo)itemInfo).damage);
                }
                GameObject blood = Instantiate(bloodVFXPrefabs, hit.point, Quaternion.LookRotation(hit.normal,Vector3.up));
                blood.transform.SetParent(hit.collider.transform);
                Destroy(blood, 2f);
            }
            pv.RPC("RPC_Shoot", RpcTarget.All, hit.point, hit.normal);
        }
    }

    [PunRPC]
    void RPC_Shoot(Vector3 hitPosition, Vector3 hitNormal)
    {
        Collider[] colliders = Physics.OverlapSphere(hitPosition, 0.3f);
        if (colliders.Length != 0)
        {
            GameObject bulletInpactObj = Instantiate(bulletImpactPrefabs, hitPosition, Quaternion.LookRotation(hitNormal, Vector3.up));
            bulletInpactObj.transform.SetParent(colliders[0].transform);
            Destroy(bulletInpactObj, 2f);
        }
    }
}
