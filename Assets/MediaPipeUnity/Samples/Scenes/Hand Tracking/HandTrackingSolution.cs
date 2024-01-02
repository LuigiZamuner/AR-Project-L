// Copyright (c) 2021 homuler
//
// Use of this source code is governed by an MIT-style
// license that can be found in the LICENSE file or at
// https://opensource.org/licenses/MIT.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mediapipe.Unity.HandTracking
{
  public class HandTrackingSolution : ImageSourceSolution<HandTrackingGraph>
  {
    [SerializeField] private DetectionListAnnotationController _palmDetectionsAnnotationController;
    [SerializeField] private NormalizedRectListAnnotationController _handRectsFromPalmDetectionsAnnotationController;
    [SerializeField] private MultiHandLandmarkListAnnotationController _handLandmarksAnnotationController;
    [SerializeField] private NormalizedRectListAnnotationController _handRectsFromLandmarksAnnotationController;

    [Header("In Game Sup")]
    public bool indexDown;
    public bool middleDown;
    public bool ringDown;
    public bool littleDown;

    public static HandTrackingSolution Instance;
    private void Awake()
    {
      Instance = this;
    }
    public HandTrackingGraph.ModelComplexity modelComplexity
    {
      get => graphRunner.modelComplexity;
      set => graphRunner.modelComplexity = value;
    }

    public int maxNumHands
    {
      get => graphRunner.maxNumHands;
      set => graphRunner.maxNumHands = value;
    }

    public float minDetectionConfidence
    {
      get => graphRunner.minDetectionConfidence;
      set => graphRunner.minDetectionConfidence = value;
    }

    public float minTrackingConfidence
    {
      get => graphRunner.minTrackingConfidence;
      set => graphRunner.minTrackingConfidence = value;
    }

    protected override void OnStartRun()
    {
      if (!runningMode.IsSynchronous())
      {
        graphRunner.OnPalmDetectectionsOutput += OnPalmDetectionsOutput;
        graphRunner.OnHandRectsFromPalmDetectionsOutput += OnHandRectsFromPalmDetectionsOutput;
        graphRunner.OnHandLandmarksOutput += OnHandLandmarksOutput;
        // TODO: render HandWorldLandmarks annotations
        graphRunner.OnHandRectsFromLandmarksOutput += OnHandRectsFromLandmarksOutput;
        graphRunner.OnHandednessOutput += OnHandednessOutput;
      }

      var imageSource = ImageSourceProvider.ImageSource;
      SetupAnnotationController(_palmDetectionsAnnotationController, imageSource, true);
      SetupAnnotationController(_handRectsFromPalmDetectionsAnnotationController, imageSource, true);
      SetupAnnotationController(_handLandmarksAnnotationController, imageSource, true);
      SetupAnnotationController(_handRectsFromLandmarksAnnotationController, imageSource, true);
    }

    protected override void AddTextureFrameToInputStream(TextureFrame textureFrame)
    {
      graphRunner.AddTextureFrameToInputStream(textureFrame);
    }

    protected override IEnumerator WaitForNextValue()
    {
      List<Detection> palmDetections = null;
      List<NormalizedRect> handRectsFromPalmDetections = null;
      List<NormalizedLandmarkList> handLandmarks = null;
      List<LandmarkList> handWorldLandmarks = null;
      List<NormalizedRect> handRectsFromLandmarks = null;
      List<ClassificationList> handedness = null;

      if (runningMode == RunningMode.Sync)
      {
        var _ = graphRunner.TryGetNext(out palmDetections, out handRectsFromPalmDetections, out handLandmarks, out handWorldLandmarks, out handRectsFromLandmarks, out handedness, true);
        if (handLandmarks != null)
        {

          if (handLandmarks[0].Landmark[8].Y > handLandmarks[0].Landmark[5].Y)// dedo abaixado
          {
            indexDown = true;
            Debug.Log("indexdown");
          }
          else
          {
            indexDown = false;
          }

          if (handLandmarks[0].Landmark[12].Y > handLandmarks[0].Landmark[9].Y)
          {
            middleDown = true;
            Debug.Log("middledown");
          }
          else
          {
            middleDown = false;
          }

          if (handLandmarks[0].Landmark[16].Y > handLandmarks[0].Landmark[13].Y)
          {
            ringDown = true;
            Debug.Log("ringdown");
          }
          else
          {
            ringDown = false;
          }

          if (handLandmarks[0].Landmark[20].Y > handLandmarks[0].Landmark[17].Y)
          {
            littleDown = true;
            Debug.Log("littledown");
          }
          else
          {
            littleDown = false;
          }
        }

      }
      else if (runningMode == RunningMode.NonBlockingSync)
      {
        yield return new WaitUntil(() => graphRunner.TryGetNext(out palmDetections, out handRectsFromPalmDetections, out handLandmarks, out handWorldLandmarks, out handRectsFromLandmarks, out handedness, false));
      }

      _palmDetectionsAnnotationController.DrawNow(palmDetections);
      _handRectsFromPalmDetectionsAnnotationController.DrawNow(handRectsFromPalmDetections);
      _handLandmarksAnnotationController.DrawNow(handLandmarks, handedness);
      // TODO: render HandWorldLandmarks annotations
      _handRectsFromLandmarksAnnotationController.DrawNow(handRectsFromLandmarks);
    }

    private void OnPalmDetectionsOutput(object stream, OutputEventArgs<List<Detection>> eventArgs)
    {
      _palmDetectionsAnnotationController.DrawLater(eventArgs.value);
    }

    private void OnHandRectsFromPalmDetectionsOutput(object stream, OutputEventArgs<List<NormalizedRect>> eventArgs)
    {
      _handRectsFromPalmDetectionsAnnotationController.DrawLater(eventArgs.value);
    }

    private void OnHandLandmarksOutput(object stream, OutputEventArgs<List<NormalizedLandmarkList>> eventArgs)
    {
      _handLandmarksAnnotationController.DrawLater(eventArgs.value);
    }

    private void OnHandRectsFromLandmarksOutput(object stream, OutputEventArgs<List<NormalizedRect>> eventArgs)
    {
      _handRectsFromLandmarksAnnotationController.DrawLater(eventArgs.value);
    }

    private void OnHandednessOutput(object stream, OutputEventArgs<List<ClassificationList>> eventArgs)
    {
      _handLandmarksAnnotationController.DrawLater(eventArgs.value);
    }
  }
}
