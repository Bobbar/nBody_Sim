#if defined(cl_khr_fp64)
#pragma OPENCL EXTENSION cl_khr_fp64: enable
#elif defined(cl_amd_fp64)
#pragma OPENCL EXTENSION cl_amd_fp64: enable
#endif
struct CUDAPrim_Struct
{
	double LocX;
	double LocY;
	double Mass;
	double SpeedX;
	double SpeedY;
	double ForceX;
	double ForceY;
};

struct CUDADebug_Struct
{
	int UB;
	int LB;
	int Other;
};


// Physics_Engine.CUDA
__kernel  void CalcPhysics(global struct CUDAPrim_Struct* Body, int BodyLen0, int nBodies, int RunThreads, global struct CUDAPrim_Struct* OutBody, int OutBodyLen0, global struct CUDADebug_Struct* DebugStuff, int DebugStuffLen0);

// Physics_Engine.CUDA
__kernel  void CalcPhysics(global struct CUDAPrim_Struct* Body, int BodyLen0, int nBodies, int RunThreads, global struct CUDAPrim_Struct* OutBody, int OutBodyLen0, global struct CUDADebug_Struct* DebugStuff, int DebugStuffLen0)
{
	int num = (int)rint((double)nBodies / (double)RunThreads);
	int num2 = nBodies - num * RunThreads;
	int x = get_local_id(0);
	bool flag = x == 0;
	int num3;
	int num4;
	if (flag)
	{
		num3 = 0;
		num4 = num;
	}
	else
	{
		num3 = num * (x - 1) + 1;
		num4 = num * x;
		bool flag2 = x == RunThreads;
		if (flag2)
		{
			num4 += num2;
		}
	}
	double num5 = 0.03;
	double num6 = 1.02;
	int num7 = num3;
	int num8 = num4;
	for (int i = num7; i <= num8; i++)
	{
		Body[(i)].ForceX = 0.0;
		Body[(i)].ForceY = 0.0;
		for (int j = 1; j <= nBodies; j++)
		{
			bool flag3 = i != j;
			if (flag3)
			{
				double num9 = Body[(j)].LocX - OutBody[(i)].LocX;
				double num10 = Body[(j)].LocY - OutBody[(i)].LocY;
				double d = num9 * num9 + num10 * num10;
				double num11 = sqrt(d);
				bool flag4 = num11 > 0.0;
				if (flag4)
				{
					double mass = OutBody[(i)].Mass;
					double mass2 = Body[(j)].Mass;
					double num12 = mass * mass2;
					double num13 = num12 / (num11 * num11 + num6 * num6);
					double num14 = num13 * num9 / num11;
					double num15 = num13 * num10 / num11;
					int expr_17C_cp_0_cp_1 = i;
					OutBody[(expr_17C_cp_0_cp_1)].ForceX = OutBody[(expr_17C_cp_0_cp_1)].ForceX + num14;
					int expr_194_cp_0_cp_1 = i;
					OutBody[(expr_194_cp_0_cp_1)].ForceY = OutBody[(expr_194_cp_0_cp_1)].ForceY + num15;
				}
			}
		}
	}
	int num16 = num3;
	int num17 = num4;
	for (int k = num16; k <= num17; k++)
	{
		int expr_1E0_cp_0_cp_1 = k;
		OutBody[(expr_1E0_cp_0_cp_1)].SpeedX = OutBody[(expr_1E0_cp_0_cp_1)].SpeedX + num5 * OutBody[(k)].ForceX / OutBody[(k)].Mass;
		int expr_216_cp_0_cp_1 = k;
		OutBody[(expr_216_cp_0_cp_1)].SpeedY = OutBody[(expr_216_cp_0_cp_1)].SpeedY + num5 * OutBody[(k)].ForceY / OutBody[(k)].Mass;
		int expr_24C_cp_0_cp_1 = k;
		OutBody[(expr_24C_cp_0_cp_1)].LocX = OutBody[(expr_24C_cp_0_cp_1)].LocX + num5 * OutBody[(k)].SpeedX;
		int expr_273_cp_0_cp_1 = k;
		OutBody[(expr_273_cp_0_cp_1)].LocY = OutBody[(expr_273_cp_0_cp_1)].LocY + num5 * OutBody[(k)].SpeedY;
	}
	DebugStuff[(get_local_id(0))].LB = num3;
        DebugStuff[(get_local_id(0))].UB = num4;
}
