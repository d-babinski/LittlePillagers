using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Society : MonoBehaviour
{
    public int Population => population;
    public int Scientists => scientists;
    public int WoodCutters => woodcutters;
    public int Miners => miners;
    public int Merchants => merchants;
    public int Farmers => farmers;

    private int population = 10;
    
    private int scientists = 0;
    private int woodcutters = 0;
    private int miners = 0;
    private int merchants = 0;
    private int farmers = 0;

    public void RaisePopulation(int _numberRaised)
    {
        population += _numberRaised;
        hireInRandomOccupation(_numberRaised);
    }
    
    public void KillPopulation(int _numberKilled)
    {
        population -= _numberKilled;
        removeRandomOccupation(_numberKilled);
    }

    public void RehirePopulation()
    {
        scientists = Mathf.FloorToInt(population * 0.25f);
        woodcutters = Mathf.FloorToInt(population* 0.15f);
        miners = Mathf.FloorToInt(population*  0.15f);
        merchants = Mathf.FloorToInt(population * 0.15f);
        farmers = Mathf.FloorToInt(population * 0.3f);

        int _stillUnemployed = getUnemployed();
        hireInRandomOccupation(_stillUnemployed);
    }

    private int getUnemployed()
    {
        return population - scientists - woodcutters - miners - merchants - farmers;
    }

    private void hireInRandomOccupation(int _count)
    {
        for (int i = 0; i < _count; i++)
        {
            changeRandomOccupation(1);
        }
    }

    private void removeRandomOccupation(int _count)
    {
        for (int i = 0; i < _count; i++)
        {
            changeRandomOccupation(-1);
        }
    }
    
    private void changeRandomOccupation(int _change)
    {
        int _chosen = Random.Range(1, 6);

        switch (_chosen)
        {
            case 1:
                scientists+=_change;
                break;
            case 2:
                woodcutters+=_change;
                break;
            case 3:
                miners+=_change;
                break;
            case 4:
                merchants+=_change;
                break;
            case 5:
                farmers+=_change;
                break;
        }
    }
}
