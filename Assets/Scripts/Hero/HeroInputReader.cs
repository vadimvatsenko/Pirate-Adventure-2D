using UnityEngine;

public class HeroInputReader : MonoBehaviour
{
    private Hero _hero;
    //[SerializeField] private Hero hero; // если мы захотим связать через редактор

    private void Awake()
    {
        _hero = GetComponent<Hero>();
    }
    private void Update()
    {
        //SetHeroDirection_V1();
        SetHeroDirection_V2();
        
        // состояние кнопок
        // Input.GetKey(KeyCode.Mouse0);  // GetKey - зажал
        // Input.GetKeyDown(KeyCode.Mouse0); // GetKeyDown - нажал
        // Input.GetKeyUp(KeyCode.Mouse0); // GetKeyUp - отпустил
        
        // Fire1 - это ссылка на кнопку в Project Settings => Input Manager в редакторе
        // Input.GetButtonDown("Fire1") // GetButtonDown - для виртуальных кнопок в Project Settings => Input Manager

        HeroFire();
    }

    private void HeroFire()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            _hero.Fire();
        }
    }

    private void SetHeroDirection_V2()
    {
        float horizontal = Input.GetAxis("Horizontal");
        _hero.SetDirection(horizontal);
    }
    
    private void SetHeroDirection_V1()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            _hero.SetDirection(-1);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            _hero.SetDirection(1);
        }
        else
        {
            _hero.SetDirection(0);
        }
    }
}
