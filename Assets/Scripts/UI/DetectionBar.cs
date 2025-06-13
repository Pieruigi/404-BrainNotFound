using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BNF.UI
{
    public class DetectionBar : MonoBehaviour
    {
        [SerializeField]
        Image bar;

        AntiSpywareNodeController targetNode;

        void Awake()
        {
            bar.fillAmount = 0;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void LateUpdate()
        {
            if (!targetNode) return;

            bar.fillAmount = targetNode.DetectionProgress;
        }

        void OnEnable()
        {
            NodeManager.OnNodeCreated += HandleOnNodeCreated;
        }

        void OnDisable()
        {
            NodeManager.OnNodeCreated -= HandleOnNodeCreated;
        }

        private void HandleOnNodeCreated(NodeController nodeController)
        {
            if (nodeController.GetType() == typeof(AntiSpywareNodeController))
                targetNode = nodeController as AntiSpywareNodeController;
        }
    }
}