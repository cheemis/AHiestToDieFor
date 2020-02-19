using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SelectedManager : MonoBehaviour
{
    private GlobalEventManager gem;

    private List<Selected> selectedRobbers;

    public List<GameObject> presetRobbers;
    private void Awake()
    {
        List<MonoBehaviour> deps = new List<MonoBehaviour>
        {
            (gem = FindObjectOfType(typeof(GlobalEventManager)) as GlobalEventManager),
        };
        if (deps.Contains(null))
        {
            throw new Exception("Could not find dependency");
        }
        selectedRobbers = new List<Selected>();
    }
    private void Start()
    {
        gem.StartListening("RightClick", MoveSelectedRobbers);
        gem.StartListening("LeftClick", SelectRobbers);
        gem.StartListening("Space", SwitchRobber);
    }
    private void OnDestroy()
    {
        gem.StopListening("RightClick", MoveSelectedRobbers);
        gem.StopListening("LeftClick", SelectRobbers);
        gem.StopListening("Space", SwitchRobber);
    }
    private void MoveSelectedRobbers(GameObject target, List<object> parameters)
    {
        if (parameters.Count == 0)
        {
            throw new Exception("Missing parameter: Could not find target location of movement");
        }
        if (parameters[0].GetType() != typeof(Vector3))
        {
            throw new Exception("Illegal argument: parameter wrong type: " + parameters[0].GetType().ToString());
        }
        foreach(Selected robber in selectedRobbers)
        {
            gem.TriggerEvent("Move", robber.go, parameters);
        }
    }
    private void SelectRobbers(GameObject target, List<object> parameters)
    {
        if (parameters.Count == 0)
        {
            throw new Exception("Missing parameter: Could not find list of robbers");
        }
        if (parameters[0].GetType() != typeof(List<GameObject>))
        {
            throw new Exception("Illegal argument: parameter wrong type: " + parameters[0].GetType().ToString());
        }
        Select((List<GameObject>) parameters[0]);
    }
    private void SwitchRobber(GameObject target, List<object> parameters)
    {
        if (selectedRobbers.Count == 0)
        {
            Select(new List<GameObject> { presetRobbers[0] });
            return;
        }
        else if (selectedRobbers.Count > 1)
        {
            Select(new List<GameObject> { presetRobbers[0] });
            return;
        }
        for (int i = 0; i < presetRobbers.Count; i++)
        {
            if (presetRobbers[i] == selectedRobbers[0].go)
            {
                Select(new List<GameObject> { presetRobbers[(i + 1) % presetRobbers.Count] });
                break;
            }
        }
    }
    private void Select(List<GameObject> robbers)
    {
        foreach(Selected robber in selectedRobbers)
        {
            robber.Reset();
        }
        selectedRobbers = robbers
            .Select(robber => new Selected(robber))
            .ToList();
        foreach(Selected robber in selectedRobbers)
        {
            robber.ApplyHighlight();
        }
    }

    private class Selected
    {
        internal GameObject go;
        Color original;

        public Selected(GameObject go)
        {
            if (go.GetComponent<MeshRenderer>() == null)
            {
                throw new Exception("Missing component: gameobject did not have MeshRenderer");
            }
            original = go.GetComponent<MeshRenderer>().material.color;
            this.go = go;
        }

        public void ApplyHighlight()
        {
            go.GetComponent<MeshRenderer>().material.color = original + Constants.highlight;
        }
        public void Reset()
        {
            go.GetComponent<MeshRenderer>().material.color = original;
        }
    }

}
