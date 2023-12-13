using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    //aşağıdaki kod ile takip edeceğimiz nesneye dışarıdan erişebiliyoruz:
    [SerializeField] GameObject thingToFollow;

    //lateupdate yapmamızın sebebi, kameranın biraz daha geç davranabilmesi, böylelikle daha yavaş ve akıcı bir kamera takibi yapabilmemiz.
    void LateUpdate()
    {
        /*
        transform.position = thingToFollow.transform.position;
        //yukarıdaki kodu yazarsak, evet kamera player'ı takip eder ama tam onun pozisyonunda olduğu için ekran kara olur. halbuki bizim kamerayı biraz geri çekmemiz gerekir.
        //bu yüzden,....
        */
        transform.position = thingToFollow.transform.position + new Vector3(0, 0, -10); //kameranın z açısını biraz geriye çekiyoruz

    }
}
