using UnityEngine;

class RandomPlacer : PipeObstacleGenerator
{
    public PipeObstacle[] obstaclePrefabs;

    public override void GenerateObstacles(Pipe pipe)
    {
        var angleStep = pipe.CurveAngle / pipe.CurveSegmentCount;
        for (var i = 0; i < pipe.CurveSegmentCount; i += 2)
        {
            var obstacle = Instantiate<PipeObstacle>(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)]);
            var pipeRotation = (Random.Range(0, pipe.pipeSegmentCount) + 0.5f) * 360f / pipe.pipeSegmentCount;
            obstacle.Position(pipe, i * angleStep, pipeRotation);
        }
    }
}

