using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UKnack;

public interface IUIShouldBeVisible
{
    public void ShouldBeVisible();
    public void ShouldBeVisible(bool visible);
    public void ShouldBeInvisible();
}
