using System.Collections;
using UnityEngine;

namespace GloomSurvivor.Scripts.Infrastructure.Interfaces
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
    }
}