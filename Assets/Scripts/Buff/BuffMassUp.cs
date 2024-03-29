using System.Collections.Generic;
using UnityEngine;


public class BuffMassUp : Buff
{
    /// Value might be changed if BuffDataset exists.
    public float bonus = 8f;
    public float defBonus = 10f;
    
    LinkedListNode<SubModifier> massToken;
    LinkedListNode<SubModifier> defToken;
    
    protected override void DatasetAdapt(BuffDataset x)
    {
        bonus = x.massupMassAdd;
        defBonus = x.defenceAdd;
    }
    
    protected override void Begin()
    {
        Unit unit = this.gameObject.GetComponent<Unit>();
        if(unit == null)
        {
            Destroy(this);
            return;
        }
        
        massToken = unit.massModifier.Add(bonus);
        defToken = unit.defenceModifier.Add(defBonus);
    }
    
    protected override void End()
    {
        if(massToken == null || defToken == null) return;
        this.gameObject.GetComponent<Unit>().massModifier.Remove(massToken);
        this.gameObject.GetComponent<Unit>().defenceModifier.Remove(defToken);
    }
    
    protected override float timeLimit { get{ return 1e20f; } }
}