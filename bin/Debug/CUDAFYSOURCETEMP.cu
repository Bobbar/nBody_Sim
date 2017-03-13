struct CUDAPrim_Struct
{
	__device__  CUDAPrim_Struct()
	{
	}
	double LocX;
	double LocY;
	double Mass;
	double SpeedX;
	double SpeedY;
	double ForceX;
	double ForceY;
	double ForceTot;
	int Color;
	double Size;
	int Visible;
	int InRoche;
	int BlackHole;
	long long UID;
	int ThreadID;
	int BlockID;
	int BlockDIM;
	int LastColID;
};


// Physics_Engine.CUDA
extern "C" __global__  void CalcPhysics( CUDAPrim_Struct* Body, int BodyLen0, double TimeStep,  CUDAPrim_Struct* OutBody, int OutBodyLen0);
// Physics_Engine.CUDA
extern "C" __global__  void CollideBodies( CUDAPrim_Struct* Body, int BodyLen0,  CUDAPrim_Struct* ColBody, int ColBodyLen0, double TimeStep);

// Physics_Engine.CUDA
extern "C" __global__  void CalcPhysics( CUDAPrim_Struct* Body, int BodyLen0, double TimeStep,  CUDAPrim_Struct* OutBody, int OutBodyLen0)
{
	int num = blockDim.x * blockIdx.x + threadIdx.x;
	double num2 = 2.0;
	bool flag = num <= BodyLen0 - 1;
	if (flag)
	{
		OutBody[(num)] = Body[(num)];
		bool flag2 = Body[(num)].Visible == 1;
		if (flag2)
		{
			OutBody[(num)].ThreadID = threadIdx.x;
			OutBody[(num)].BlockID = blockIdx.x;
			OutBody[(num)].BlockDIM = blockDim.x;
			OutBody[(num)].ForceX = 0.0;
			OutBody[(num)].ForceY = 0.0;
			OutBody[(num)].ForceTot = 0.0;
			int num3 = BodyLen0 - 1;
			for (int i = 0; i <= num3; i++)
			{
				bool flag3 = num != i & Body[(i)].Visible == 1;
				if (flag3)
				{
					double num4 = Body[(i)].LocX - OutBody[(num)].LocX;
					double num5 = Body[(i)].LocY - OutBody[(num)].LocY;
					double d = num4 * num4 + num5 * num5;
					double num6 = sqrt(d);
					bool flag4 = num6 > 0.0;
					if (flag4)
					{
						double mass = OutBody[(num)].Mass;
						double mass2 = Body[(i)].Mass;
						double num7 = mass * mass2;
						double num8 = num7 / (num6 * num6 + num2 * num2);
						double num9 = num8 * num4 / num6;
						double num10 = num8 * num5 / num6;
						int expr_1D1_cp_0_cp_1 = num;
						OutBody[(expr_1D1_cp_0_cp_1)].ForceTot = OutBody[(expr_1D1_cp_0_cp_1)].ForceTot + num8;
						int expr_1E6_cp_0_cp_1 = num;
						OutBody[(expr_1E6_cp_0_cp_1)].ForceX = OutBody[(expr_1E6_cp_0_cp_1)].ForceX + num9;
						int expr_1FB_cp_0_cp_1 = num;
						OutBody[(expr_1FB_cp_0_cp_1)].ForceY = OutBody[(expr_1FB_cp_0_cp_1)].ForceY + num10;
					}
				}
			}
			__syncthreads();
		}
	}
}
// Physics_Engine.CUDA
extern "C" __global__  void CollideBodies( CUDAPrim_Struct* Body, int BodyLen0,  CUDAPrim_Struct* ColBody, int ColBodyLen0, double TimeStep)
{
	int num = blockDim.x * blockIdx.x + threadIdx.x;
	bool flag = num <= BodyLen0 - 1 & Body[(num)].Visible == 1;
	if (flag)
	{
		ColBody[(num)] = Body[(num)];
		int num2 = BodyLen0 - 1;
		for (int i = 0; i <= num2; i++)
		{
			bool flag2 = num != i & Body[(i)].Visible == 1;
			if (flag2)
			{
				double num3 = Body[(i)].LocX - ColBody[(num)].LocX;
				double num4 = Body[(i)].LocY - ColBody[(num)].LocY;
				double d = num3 * num3 + num4 * num4;
				double num5 = sqrt(d);
				bool flag3 = num5 <= ColBody[(num)].Size / 2.0 + Body[(i)].Size / 2.0;
				if (flag3)
				{
					ColBody[(num)].LastColID = i;
					bool flag4 = num5 > 0.0;
					if (flag4)
					{
						double speedX = ColBody[(num)].SpeedX;
						double speedY = ColBody[(num)].SpeedY;
						double speedX2 = Body[(i)].SpeedX;
						double speedY2 = Body[(i)].SpeedY;
						double mass = ColBody[(num)].Mass;
						double mass2 = Body[(i)].Mass;
						double num6 = num3 / 2.0;
						double num7 = num4 / 2.0;
						num6 /= num5 / 2.0;
						num7 /= num5 / 2.0;
						double num8 = num6 * speedX + num7 * speedY;
						double num9 = num6 * speedX2 + num7 * speedY2;
						double num10 = (mass * num8 + mass2 * num9 - mass2 * (num8 - num9)) / (mass + mass2);
						double num11 = (mass * num8 + mass2 * num9 - mass * (num9 - num8)) / (mass + mass2);
						bool flag5 = ColBody[(num)].InRoche == 0 & Body[(i)].InRoche == 1;
						if (flag5)
						{
							bool flag6 = ColBody[(num)].Mass > Body[(i)].Mass;
							if (flag6)
							{
								double speedX3 = ColBody[(num)].SpeedX;
								double speedY3 = ColBody[(num)].SpeedY;
								ColBody[(num)].SpeedX = ColBody[(num)].SpeedX + (num10 - num8) * num6;
								ColBody[(num)].SpeedY = ColBody[(num)].SpeedY + (num10 - num8) * num7;
								Body[(i)].Visible = 0;
								double num12 = 3.14159265358979 * pow(ColBody[(num)].Size, 2.0);
								double num13 = 3.14159265358979 * pow(Body[(i)].Size, 2.0);
								num12 += num13;
								ColBody[(num)].Size = sqrt(num12 / 3.14159265358979);
								ColBody[(num)].Mass = ColBody[(num)].Mass + Body[(i)].Mass;
							}
							else
							{
								bool flag7 = ColBody[(num)].Mass == Body[(i)].Mass;
								if (flag7)
								{
									bool flag8 = ColBody[(num)].UID > Body[(i)].UID;
									if (flag8)
									{
										double speedX3 = ColBody[(num)].SpeedX;
										double speedY3 = ColBody[(num)].SpeedY;
										ColBody[(num)].SpeedX = ColBody[(num)].SpeedX + (num10 - num8) * num6;
										ColBody[(num)].SpeedY = ColBody[(num)].SpeedY + (num10 - num8) * num7;
										Body[(i)].Visible = 0;
										double num12 = 3.14159265358979 * pow(ColBody[(num)].Size, 2.0);
										double num13 = 3.14159265358979 * pow(Body[(i)].Size, 2.0);
										num12 += num13;
										ColBody[(num)].Size = sqrt(num12 / 3.14159265358979);
										ColBody[(num)].Mass = ColBody[(num)].Mass + Body[(i)].Mass;
									}
									else
									{
										ColBody[(num)].Visible = 0;
									}
								}
							}
						}
						else
						{
							bool flag9 = ColBody[(num)].InRoche == 0 & Body[(i)].InRoche == 0;
							if (flag9)
							{
								bool flag10 = ColBody[(num)].Mass > Body[(i)].Mass;
								if (flag10)
								{
									double speedX3 = ColBody[(num)].SpeedX;
									double speedY3 = ColBody[(num)].SpeedY;
									ColBody[(num)].SpeedX = ColBody[(num)].SpeedX + (num10 - num8) * num6;
									ColBody[(num)].SpeedY = ColBody[(num)].SpeedY + (num10 - num8) * num7;
									Body[(i)].Visible = 0;
									double num12 = 3.14159265358979 * pow(ColBody[(num)].Size, 2.0);
									double num13 = 3.14159265358979 * pow(Body[(i)].Size, 2.0);
									num12 += num13;
									ColBody[(num)].Size = sqrt(num12 / 3.14159265358979);
									ColBody[(num)].Mass = ColBody[(num)].Mass + Body[(i)].Mass;
								}
								else
								{
									bool flag11 = ColBody[(num)].Mass == Body[(i)].Mass;
									if (flag11)
									{
										bool flag12 = ColBody[(num)].UID > Body[(i)].UID;
										if (flag12)
										{
											double speedX3 = ColBody[(num)].SpeedX;
											double speedY3 = ColBody[(num)].SpeedY;
											ColBody[(num)].SpeedX = ColBody[(num)].SpeedX + (num10 - num8) * num6;
											ColBody[(num)].SpeedY = ColBody[(num)].SpeedY + (num10 - num8) * num7;
											Body[(i)].Visible = 0;
											double num12 = 3.14159265358979 * pow(ColBody[(num)].Size, 2.0);
											double num13 = 3.14159265358979 * pow(Body[(i)].Size, 2.0);
											num12 += num13;
											ColBody[(num)].Size = sqrt(num12 / 3.14159265358979);
											ColBody[(num)].Mass = ColBody[(num)].Mass + Body[(i)].Mass;
										}
										else
										{
											ColBody[(num)].Visible = 0;
										}
									}
									else
									{
										ColBody[(num)].Visible = 0;
									}
								}
							}
							else
							{
								bool flag13 = ColBody[(num)].InRoche == 1 & Body[(i)].InRoche == 1;
								if (flag13)
								{
									mass = ColBody[(num)].Mass;
									mass2 = Body[(i)].Mass;
									double num14 = mass * mass2;
									double num15 = 1.02;
									double num16 = num14 / (num5 * num5 + num15 * num15);
									double num17 = num16 * num3 / num5;
									double num18 = num16 * num4 / num5;
									int num19 = 40;
									int expr_85D_cp_0_cp_1 = num;
									ColBody[(expr_85D_cp_0_cp_1)].ForceX = ColBody[(expr_85D_cp_0_cp_1)].ForceX - num17 * (double)num19;
									int expr_878_cp_0_cp_1 = num;
									ColBody[(expr_878_cp_0_cp_1)].ForceY = ColBody[(expr_878_cp_0_cp_1)].ForceY - num18 * (double)num19;
									double num20 = 0.1;
									int expr_89E_cp_0_cp_1 = num;
									ColBody[(expr_89E_cp_0_cp_1)].SpeedX = ColBody[(expr_89E_cp_0_cp_1)].SpeedX + (num10 - num8) * num6 * num20;
									int expr_8BD_cp_0_cp_1 = num;
									ColBody[(expr_8BD_cp_0_cp_1)].SpeedY = ColBody[(expr_8BD_cp_0_cp_1)].SpeedY + (num10 - num8) * num7 * num20;
								}
								else
								{
									bool flag14 = ColBody[(num)].InRoche == 1 & Body[(i)].InRoche == 0;
									if (flag14)
									{
										ColBody[(num)].Visible = 0;
									}
								}
							}
						}
					}
					else
					{
						bool flag15 = ColBody[(num)].Mass > Body[(i)].Mass;
						if (flag15)
						{
							double num12 = 3.14159265358979 * pow(ColBody[(num)].Size, 2.0);
							double num13 = 3.14159265358979 * pow(Body[(i)].Size, 2.0);
							num12 += num13;
							ColBody[(num)].Size = sqrt(num12 / 3.14159265358979);
							ColBody[(num)].Mass = ColBody[(num)].Mass + Body[(i)].Mass;
						}
						else
						{
							ColBody[(num)].Visible = 0;
						}
					}
				}
			}
		}
		int expr_A03_cp_0_cp_1 = num;
		ColBody[(expr_A03_cp_0_cp_1)].SpeedX = ColBody[(expr_A03_cp_0_cp_1)].SpeedX + TimeStep * ColBody[(num)].ForceX / ColBody[(num)].Mass;
		int expr_A35_cp_0_cp_1 = num;
		ColBody[(expr_A35_cp_0_cp_1)].SpeedY = ColBody[(expr_A35_cp_0_cp_1)].SpeedY + TimeStep * ColBody[(num)].ForceY / ColBody[(num)].Mass;
		int expr_A67_cp_0_cp_1 = num;
		ColBody[(expr_A67_cp_0_cp_1)].LocX = ColBody[(expr_A67_cp_0_cp_1)].LocX + TimeStep * ColBody[(num)].SpeedX;
		int expr_A8B_cp_0_cp_1 = num;
		ColBody[(expr_A8B_cp_0_cp_1)].LocY = ColBody[(expr_A8B_cp_0_cp_1)].LocY + TimeStep * ColBody[(num)].SpeedY;
	}
	__syncthreads();
}
