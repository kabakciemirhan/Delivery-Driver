using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriverController : MonoBehaviour
{
    //serialize field dersinden sonra bu değişkenlere serializefield ekleyerek inspector üzerinden erişimlerini sağlıyoruz.
    [SerializeField] float steerSpeed = 1f; //float tipinde yazdığımız için "f" harfi eklemesek de olur ama ben yine de ekledim.
    [SerializeField] float moveSpeed = 0.1f; //float tipinde yazdığımız için "f" harfi eklemesek de olur ama ben yine de ekledim.

    //boosts and bumps
    [SerializeField] float slowSpeed = 3f; //editördeki movespeed değerini verdim.
    [SerializeField] float boostSpeed = 10f;

    public FixedJoystick _joystick;

    void Update()
    {

    //transform rotate ve transform translate kodlarından sonra; Edit > Project Settings > Input manager alanındaki horizontal kısmını buraya getirmek için yazdığım kod aşağıda: Bu bize sağ sol girişini yapmamızı sağlayacak
    //float steerAmount = Input.GetAxis("Horizontal") * steerSpeed * Time.deltaTime; //dönme hızını da steerspeed ile belirleyebiliriz.
    float steerAmount = _joystick.Horizontal * steerSpeed * Time.deltaTime; //dönme hızını da steerspeed ile belirleyebiliriz.

    //ikinci olarak, Edit > Project Settings > Input manager alanındaki vertical kısmını buraya getirerek yukarı ok aşağı ok tuşları (ya da oyun kolu, dokunmatik yani kontrol her ne ise) ileri geri kontrolünü sağlayabiliriz.
    //float moveAmount = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime; //hareket hızını da movespeed ile belirleyebiliriz.
    float moveAmount = _joystick.Vertical * moveSpeed * Time.deltaTime;

    //time.deltatime ile çarpma işlemi bize hareketlerin daha yumuşak şekilde olmasını, frame independent bir hareket sağlıyor.

    //update metodunu kullanarak her frame başı yavaş yavaş objeyi döndürüyoruz
    //x'i döndürme, y'yi döndürme, z yi her frame başı 3f boyutunda yavaş yavaş döndür.

    //değişkensiz transform.rotate ==> transform.Rotate(0, 0, 3f);
    //yeni transform rotate(değişkenli):
    transform.Rotate(0, 0, -steerAmount);
    /*- koymamızın sebebi şu, float değeri - değere düştükçe araç sürekli sola gidecek çünkü - değer left demek, + değer right demek.
     bunu çözmek için, - ye düşüş yaşandığında - ile çarpıp tekrar + değere getirebilir, + değerden - çıkararak normal olarak sağ sol dengesini
    kurabiliriz.*/

    //---

    //aracı hareket ettirelim. yukarı doğru hareket etmesini istiyoruz, bu yüzden y'ye müdahale edeceğiz.
    //aracımız aynı zamanda yukarıdaki kod ile dönüş yaptığından yavaş bir daire çizecek

    //değişkensiz transform.translate ==> transform.Translate(0, 0.1f, 0);
    //yeni transform.translate(değişkenli):
    transform.Translate(0, moveAmount, 0);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //boost gerçekleştiğinde
        if(collision.tag == "Boost")
        {
            Debug.Log("you are boosting man!");
            moveSpeed = boostSpeed; //hemen araba hızımızı boostspeed e eşitledik
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        moveSpeed = slowSpeed; //bu kod yapısının anlamı, eğer aracımız herhangi bir şeye çarparsa hızı bir anda normale dönsün.
    }
}

