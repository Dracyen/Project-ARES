using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NewPlayerDrive : MonoBehaviour
{
   
    public NavMeshSurface Track;
    NavMeshAgent playerCar;
    Rigidbody rb;
    public float maxSpeed;
    public Transform target;
    float xPos;
    public GameObject model3D;
    public GameObject wheel1;
    public GameObject wheel2;
    RaycastHit hit;
    public bool RaceiIsOnGoing = true;

    private void Awake()
    {
        GenerateNavMesh();
    }
    void Start()
    {
        
        playerCar = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("PlayerTarget").transform;
    }
    public void GenerateNavMesh()
    {
        //Procura pelo objeto "Pai" na qual a track sera instanciada para buscar como referencia para o NavMesh
        Track = GameObject.FindGameObjectWithTag("Track").GetComponent<NavMeshSurface>();
        Track.BuildNavMesh();
       

    }
    
    
    void Update()
    {
        //Checar a inclinacao da pista com um RayCast
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit))
        {         
            //Aproxima o modelo do carro e o target do chao para evitar aparencia de voo
           // model3D.transform.localPosition = new Vector3(model3D.transform.localPosition.x, - hit.distance/2, model3D.transform.localPosition.z);
            //target.transform.localPosition = new Vector3(target.transform.localPosition.x, -hit.distance/2, target.transform.localPosition.z);
            //Determina o angulo de acordo com a normal do plano
            float angleUp = Vector3.Angle(hit.normal, transform.up);
            float angleFoward = Vector3.Angle(hit.normal, transform.forward);
            //Avalia se o carro esta subindo ou descendo ao avaliar o angulo de caordo com o vetor frontal do carro
            if (angleFoward < 90)
            {
                //transform.Rotate(new Vector3(angleUp, 0, 0));
                
            }
            else
            {
                //transform.Rotate(new Vector3(-angleUp, 0, 0));
            }
            
        }
        //Garante que o modelo do carro esta olhando para o target e o acompanhara em caso de curvas
        model3D.transform.LookAt(target.transform);
        if (Input.touchCount > 0)
        {
            if (Input.touches[0].position.x < Screen.width / 2 || Input.GetKey(KeyCode.A))
            {
                //Debug.Log("L");
                MoveR();
            }
            else if(Input.touches[0].phase == TouchPhase.Ended)
            {
                UnMoveR();
            }

            else if (Input.touches[0].position.x > Screen.width / 2 || Input.GetKey(KeyCode.D))
            {
                //Debug.Log("R");
                MoveL();
            }
            else if (Input.touches[0].phase == TouchPhase.Ended)
            {
                UnMoveL();
            }
        }
        else
        {
            UnMove();
        }
        //Input para virar a direita
        if (Input.GetKey(KeyCode.D))
        {
            //Ao reposicionar o Target o carro faz a curva parecendo um drift
            if(target.transform.localPosition.x < 1 && Input.GetKey(KeyCode.Space))
            {
                target.transform.localPosition += new Vector3(0.01f, 0, 0);
            }
            else
            {
            //Caso nao tenha a intencao de fazer a curva com drift o carro apenas ira rodar em seu eixo acompanhado pelas rodas
                transform.Rotate(new Vector3(0, 0.5f, 0));
                if(wheel1.transform.localRotation.y < 0.3f)
                {
                    wheel1.transform.Rotate(new Vector3(0, 0.5f, 0));
                    wheel2.transform.Rotate(new Vector3(0, 0.5f, 0));
                }
               
            }
            
        }
        // Quando deixa de virar o target volta a posicao original
        else if (!Input.GetKeyUp(KeyCode.D))
        {
            if (target.transform.localPosition.x > 0)
            {
                target.transform.localPosition -= new Vector3(0.01f, 0, 0);
            }
  
        }
        // O mesmo ocorre ao virar para a esquerda
        if (Input.GetKey(KeyCode.A))
        {
            if (target.transform.localPosition.x > -1 && Input.GetKey(KeyCode.Space))
            {
               
                target.transform.localPosition -= new Vector3(0.01f, 0, 0);
            }
            else
            {
                transform.Rotate(new Vector3(0, -0.5f, 0)); 
                if (wheel1.transform.localRotation.y > -0.3f)
                {
                    wheel1.transform.Rotate(new Vector3(0, -0.5f, 0));
                    wheel2.transform.Rotate(new Vector3(0, -0.5f, 0));
                }
            }
        }
        else if (!Input.GetKeyUp(KeyCode.A))
        {
            if (target.transform.localPosition.x < 0)
            {
                target.transform.localPosition += new Vector3(0.01f, 0, 0);
            }
        }
        if (RaceiIsOnGoing)
        {
            //O carro anda sempre para frente independente da sua rotacao ao seguir o target
            playerCar.destination = target.position;
        }
        
        //Caso nao esteja virando para nenhum lado a roda deve voltar ao centro
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            if (wheel1.transform.localRotation.y != 0)
            {
                wheel1.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
                wheel2.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
        }
    }

    void MoveR()
    {
        if (target.transform.localPosition.x > -1 && Input.GetKey(KeyCode.Space))
        {

            target.transform.localPosition -= new Vector3(0.01f, 0, 0);
        }
        else
        {
            transform.Rotate(new Vector3(0, -5f, 0));
            if (wheel1.transform.localRotation.y > -0.3f)
            {
                wheel1.transform.Rotate(new Vector3(0, -5f, 0));
                wheel2.transform.Rotate(new Vector3(0, -5f, 0));
            }
        }
    }
    void UnMoveR()
    {
        if (target.transform.localPosition.x < 0)
        {
            target.transform.localPosition += new Vector3(0.01f, 0, 0);
        }
    }
    void MoveL()
    {
        //Ao reposicionar o Target o carro faz a curva parecendo um drift
        if (target.transform.localPosition.x < 1 && Input.GetKey(KeyCode.Space))
        {
            target.transform.localPosition += new Vector3(0.01f, 0, 0);
        }
        else
        {
            //Caso nao tenha a intencao de fazer a curva com drift o carro apenas ira rodar em seu eixo acompanhado pelas rodas
            transform.Rotate(new Vector3(0, 5f, 0));
            if (wheel1.transform.localRotation.y < 0.3f)
            {
                wheel1.transform.Rotate(new Vector3(0, 5f, 0));
                wheel2.transform.Rotate(new Vector3(0, 5f, 0));
            }

        }
    }
    void UnMoveL()
    {
        if (target.transform.localPosition.x > 0)
        {
            target.transform.localPosition -= new Vector3(0.01f, 0, 0);
        }
    }
    void UnMove()
    {
        if (wheel1.transform.localRotation.y != 0)
        {
            wheel1.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            wheel2.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
