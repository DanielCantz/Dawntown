using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStatModificator
{
    float Modify(float value);

    float Demodify(float value);
}
