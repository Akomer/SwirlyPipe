using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSystem : MonoBehaviour
{

    public Pipe pipePrefab;

    public int pipeCount;
    public int emptyPipeCount;

    private Pipe[] pipes;

    private void Awake()
    {
        pipes = new Pipe[pipeCount];
        for (var i = 0; i < pipes.Length; i++)
        {
            var pipe = pipes[i] = Instantiate<Pipe>(pipePrefab);
            pipe.transform.SetParent(transform, false);
            pipe.Generate(i > emptyPipeCount);
            if (i > 0)
            {
                pipe.AlignWith(pipes[i - 1]);
            }
        }
    }

    public Pipe SetupFirstPipe()
    {
        for (var i = 0; i < pipes.Length; i++)
        {
            Pipe pipe = pipes[i];
            pipe.Generate(i > emptyPipeCount);
            if (i > 0)
            {
                pipe.AlignWith(pipes[i - 1]);
            }
        }
        AlignNextPipeWithOrigin();
        transform.localPosition = new Vector3(0f, -pipes[0].CurveRadius);
        return pipes[0];
    }

    internal Pipe SetupNextPipe()
    {
        ShiftPipes();
        AlignNextPipeWithOrigin();
        pipes[pipes.Length - 1].Generate();
        pipes[pipes.Length - 1].AlignWith(pipes[pipes.Length - 2]);
        transform.localPosition = new Vector3(0f, -pipes[0].CurveRadius);
        return pipes[0];
    }

    private void ShiftPipes()
    {
        Pipe temp = pipes[0];
        for (var i = 1; i < pipes.Length; i++)
        {
            pipes[i - 1] = pipes[i];
        }
        pipes[pipes.Length - 1] = temp;
    }

    private void AlignNextPipeWithOrigin()
    {
        Transform transformToAlign = pipes[0].transform;
        for (var i = 1; i < pipes.Length; i++)
        {
            pipes[i].transform.SetParent(transformToAlign);
        }

        transformToAlign.localPosition = Vector3.zero;
        transformToAlign.localRotation = Quaternion.identity;

        for (var i = 1; i < pipes.Length; i++)
        {
            pipes[i].transform.SetParent(transform);
        }
    }
}
