using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] Color32 hasPackageColor = new Color32(1, 1, 1, 1); //bu beyaz renk demektir. Arabamız paket alınca bu renge bürünecektir. Serializefield olarak yazdığımız için editör kısmından bunlara erişebiliriz.
    [SerializeField] Color32 noPackageColor = new Color32(1, 1, 1, 1); //yine beyaz renk yaptık çünkü nasıl olsa editör üzerinden değişiklik yapacağız. aracımız paket taşımıyorken bu renge bürünecektir.

    [SerializeField] float destroyDelay = 0.5f; //ne kadar zaman sonra yok olsun?
    bool hasPackage; //default değer her zaman sıfırdır

    SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();  //oyun başlar başlamaz aracımızın spriterenderer'ına eriştik. bu kod, aracımıza bağlı olduğu için ekstradan gameobject find yapmamıza gerek kalmadı.  
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //bu kod yapısı, çarpışma gerçekleşti demektir.
        Debug.Log("Ouch!");
        //çarptığımız şey istrigger olarak işaretli değilse debuglog burda devreye girmiş olacaktır.
    }
      
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //bu kod yapısı üzerinden geçme işi gerçekleşti demektir.
        //Debug.Log("what the fuck is that?");

        /*if (the thing we trigger is the package)
         {
            then print "package picked up" to the console
         }
        */

        //bu koddan önce, çarpacağımız gameobject tagını "package" olarak ayarlamamız gerekiyor

        if(collision.tag == "Package" && hasPackage == false) //eğer başka pakete sahip değilsek ? Yani amaç aynı anda birden fazla paket taşıttırmamak olmalı.
                                                              //yukarıdaki kodun aynısını aşağıdaki şekilde de yazabiliriz
                                                              //if(collision.tag == "Package" && !hasPackage)
        {
            Debug.Log("package picked up");
            hasPackage = true; //paketi aldığımızı bool değerine atadık.
            spriteRenderer.color = hasPackageColor; //aracın rengini belirlediğim haspackagecolor rengine bürü.
            Destroy(collision.gameObject, destroyDelay); //hedef gameobject, 0.5 saniye sonra kendini yok etsin.
        }

        if((collision.tag == "Customer") && (hasPackage == true)) //eğer paketi aldıysak ve çarptığımız nesne Customer ise;
                                                                  //yukarıdaki kodun aynısını aşağıdaki şekilde de yazabiliriz
                                                                  //if(collision.tag == "Package" && hasPackage)
        {
            Debug.Log("package delivered to customer SUCCESSFULLY!" );
            hasPackage = false; //paketi teslim ettik, elimizde paket kalmadı demenin farklı bir yolu
            spriteRenderer.color = noPackageColor; //aracın rengini nopackagecolor rengine bürü
        }
    }
}
