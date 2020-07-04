using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculadora : MonoBehaviour
{
    int entero = 2;
    float soyUnDecimal = 2.5f;
    bool booleano = true;
    char caracter = 'a';
    string cadena = "Rogelio Trejo Perez - Alias: Rojo";


    // Start is called before the first frame update
    void Start()
    {
        //print(entero);
        entero = SumaReturn(30, 25);
        //print(entero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* void MiPrimeraFuncion()
    {
        int entero = 4800;
        print(cadena);
        print(entero);
        print(caracter);
        print(soyUnDecimal);
    }  */

    public void SumaInternaSI(int a)
    {
        int c = a + 20;
        print(c);
    }

    void Suma(int a, int b)
    {
        int c = a - b;
        print(c);
    }

    void Suma(float a, float b)
    {
        float c = a - b;
        print(c);
    }

    int SumaReturn (int a, int b)
    {
        int c = a + b;
        print(c);
        return c;
    }

    void Division(int a, int b)
    {
        int c = a / b;
        print(c);
    }
}
