//    private float clerp(float start, float end, float value)
//    {
//        float min = 0.0f;
//        float max = 360.0f;
//        float half = Mathf.Abs((max - min) * 0.5f);
//        float retval = 0.0f;
//        float diff = 0.0f;
//        if ((end - start) < -half)
//        {
//            diff = ((max - start) + end) * value;
//            retval = start + diff;
//        }
//        else if ((end - start) > half)
//        {
//            diff = -((max - end) + start) * value;
//            retval = start + diff;
//        }
//        else retval = start + (end - start) * value;
//        return retval;
//    }

//    private float spring(float start, float end, float value)
//    {
//        value = Mathf.Clamp01(value);
//        value = (Mathf.Sin(value * Mathf.PI * (0.2f + 2.5f * value * value * value)) * Mathf.Pow(1f - value, 2.2f) + value) * (1f + (1.2f * (1f - value)));
//        return start + (end - start) * value;
//    }

//    private float punch(float amplitude, float value)
//    {
//        float s = 9;
//        if (value == 0)
//        {
//            return 0;
//        }
//        else if (value == 1)
//        {
//            return 0;
//        }
//        float period = 1 * 0.3f;
//        s = period / (2 * Mathf.PI) * Mathf.Asin(0);
//        return (amplitude * Mathf.Pow(2, -10 * value) * Mathf.Sin((value * 1 - s) * (2 * Mathf.PI) / period));
//    }

//    