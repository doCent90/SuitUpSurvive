using UnityEngine;
using UnityEngine.AI;

namespace Assets.Source
{
    public abstract class CharacterPresenter : Presenter
    {
        [SerializeField] protected Transform Model;
        [SerializeField] protected Animator Animator;
        [SerializeField] protected NavMeshAgent Agent;
    }
}
