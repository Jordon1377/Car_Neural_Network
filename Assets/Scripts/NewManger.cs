using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewManger : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject refrence;

    private bool isTraning = false;
    private int populationSize = 20;
    public static int generationNumber = 0;
    private int[] layers = new int[] { 6, 3,3,3, 3}; //1 input and 1 output
    public static List<NeuralNetwork> nets;
    public static List<Cars> carList = null;

    private float timer = 35f;
    public static int index;


    void Timer()
    {
        isTraning = false;
    }


	void Update ()
    {
        if (isTraning == false)
        {
            if (generationNumber == 0)
            {
                InitCarNeuralNetworks();
            }
            
            if (generationNumber > 100) {

                float fit = -10000000;
                for (int i = 0; i < populationSize; i++)
                {
                    
                    if (fit <= nets[i].GetFitness()) {
                        fit = nets[i].GetFitness();
                        index = i;
                        Debug.Log("The fitness is" + fit);
                    }
                }
                for (int i = 0; i < populationSize; i++)
                {
                    nets[i] = nets[index];
                }

                nets.Sort();
                for (int i = 0; i < populationSize / 2; i++)
                {
                    nets[i] = new NeuralNetwork(nets[i+(populationSize / 2)]);
                    nets[i].Mutate(1);

                    nets[i + (populationSize / 2)] = new NeuralNetwork(nets[i + (populationSize / 2)]); //too lazy to write a reset neuron matrix values method....so just going to make a deepcopy lol
                }

                for (int i = 0; i < populationSize; i++)
                {
                    nets[i].SetFitness(0f);
                }
                
                //timer = 10000f;


            }
            
             if (generationNumber > 150) {
                 
                float fit = -10000000;
                for (int i = 0; i < populationSize; i++)
                {
                    
                    if (fit <= nets[i].GetFitness()) {
                        fit = nets[i].GetFitness();
                        index = i;
                        //Debug.Log("The highest fitness is" + fit);
                    }
                }
                for (int i = 0; i < populationSize; i++)
                {
                    nets[i] = nets[index];
                }

                nets.Sort();
                for (int i = 0; i < populationSize / 2; i++)
                {
                    nets[i] = new NeuralNetwork(nets[i+(populationSize / 2)]);
                    //nets[i].Mutate(1);

                    nets[i + (populationSize / 2)] = new NeuralNetwork(nets[i + (populationSize / 2)]); //too lazy to write a reset neuron matrix values method....so just going to make a deepcopy lol
                }

                for (int i = 0; i < populationSize; i++)
                {
                    nets[i].SetFitness(0f);
                }

             }
            else
            {
                nets.Sort();
                float fit = -10000000;
                for (int i = 0; i < populationSize / 2; i++)
                {

                    if (fit <= nets[i].GetFitness()) {
                        fit = nets[i].GetFitness();
                        index = i;
                        
                    }
                }
                Debug.Log("The highest fitness is " + fit);
                for (int i = 0; i < populationSize / 2; i++)
                {                   

                    nets[i] = new NeuralNetwork(nets[i+(populationSize / 2)]);
                    
                    //if (i != index) {
                    nets[i].Mutate(2);
                    //}

                    nets[i + (populationSize / 2)] = new NeuralNetwork(nets[i + (populationSize / 2)]); //too lazy to write a reset neuron matrix values method....so just going to make a deepcopy lol
                }

                for (int i = 0; i < populationSize; i++)
                {
                    nets[i].SetFitness(0f);
                }
            }

            
            generationNumber++;
            
            isTraning = true;
            Invoke("Timer",timer);
            CreateCarBodies();
            Debug.Log(generationNumber);
        }

    }


    private void CreateCarBodies()
    {
        if (carList != null)
        {
            for (int i = 0; i < carList.Count; i++)
            {
                GameObject.Destroy(carList[i].gameObject);
            }

        }

        carList = new List<Cars>();

        for (int i = 0; i < populationSize; i++)
        {
            Cars car = ((GameObject)Instantiate(carPrefab, refrence.transform.position,carPrefab.transform.rotation)).GetComponent<Cars>();
            car.Init(nets[i], refrence.transform);
            carList.Add(car);
        }

    }

    void InitCarNeuralNetworks()
    {
        //population must be even, just setting it to 20 incase it's not
        if (populationSize % 2 != 0)
        {
            populationSize = 20; 
        }

        nets = new List<NeuralNetwork>();
        

        for (int i = 0; i < populationSize; i++)
        {
            NeuralNetwork net = new NeuralNetwork(layers);
            net.Mutate(2);
            nets.Add(net);
        }
    }
}
