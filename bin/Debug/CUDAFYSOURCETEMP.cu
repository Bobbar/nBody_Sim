struct CUDAPrim_Struct
{
	__device__  CUDAPrim_Struct()
	{
	}
	double LocX;
	double LocY;
	float Mass;
	float SpeedX;
	float SpeedY;
	float ForceX;
	float ForceY;
	float ForceTot;
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
extern "C" __global__  void CalcPhysics( CUDAPrim_Struct* Body, int BodyLen0, float TimeStep,  CUDAPrim_Struct* OutBody, int OutBodyLen0);
// Physics_Engine.CUDA
extern "C" __global__  void CollideBodies( CUDAPrim_Struct* Body, int BodyLen0,  CUDAPrim_Struct* ColBody, int ColBodyLen0, float TimeStep);

// Physics_Engine.CUDA
extern "C" __global__  void CalcPhysics( CUDAPrim_Struct* Body, int BodyLen0, float TimeStep,  CUDAPrim_Struct* OutBody, int OutBodyLen0)
{
	int num = blockDim.x * blockIdx.x + threadIdx.x;
	float num2 = 2.0f;
	bool flag = num <= BodyLen0 - 1;
	if (flag)
	{
		OutBody[(num)] = Body[(num)];
		bool flag2 = Body[(num)].Visible == 1;
		if (flag2)
		{
			OutBody[(num)].ForceX = 0.0f;
			OutBody[(num)].ForceY = 0.0f;
			OutBody[(num)].ForceTot = 0.0f;
			int num3 = BodyLen0 - 1;
			for (int i = 0; i <= num3; i++)
			{
				bool flag3 = num != i & Body[(i)].Visible == 1;
				if (flag3)
				{
					float num4 = (float)(Body[(i)].LocX - OutBody[(num)].LocX);
					float num5 = (float)(Body[(i)].LocY - OutBody[(num)].LocY);
					float num6 = num4 * num4 + num5 * num5;
					float num7 = (float)sqrt((double)num6);
					bool flag4 = num7 > 0.0f;
					if (flag4)
					{
						float mass = OutBody[(num)].Mass;
						float mass2 = Body[(i)].Mass;
						float num8 = mass * mass2;
						float num9 = num8 / (num7 * num7 + num2 * num2);
						float num10 = num9 * num4 / num7;
						float num11 = num9 * num5 / num7;
						int expr_17C_cp_0_cp_1 = num;
						OutBody[(expr_17C_cp_0_cp_1)].ForceTot = OutBody[(expr_17C_cp_0_cp_1)].ForceTot + num9;
						int expr_191_cp_0_cp_1 = num;
						OutBody[(expr_191_cp_0_cp_1)].ForceX = OutBody[(expr_191_cp_0_cp_1)].ForceX + num10;
						int expr_1A6_cp_0_cp_1 = num;
						OutBody[(expr_1A6_cp_0_cp_1)].ForceY = OutBody[(expr_1A6_cp_0_cp_1)].ForceY + num11;
					}
				}
			}
			bool flag5 = OutBody[(num)].ForceTot > OutBody[(num)].Mass * 4.0f & OutBody[(num)].BlackHole == 0;
			if (flag5)
			{
				OutBody[(num)].InRoche = 1;
			}
			else
			{
				bool flag6 = OutBody[(num)].ForceTot * 2.0f < OutBody[(num)].Mass * 4.0f;
				if (flag6)
				{
					OutBody[(num)].InRoche = 0;
				}
				else
				{
					bool flag7 = OutBody[(num)].BlackHole == 2;
					if (flag7)
					{
						OutBody[(num)].InRoche = 1;
					}
				}
			}
		}
	}
}
// Physics_Engine.CUDA
extern "C" __global__  void CollideBodies( CUDAPrim_Struct* Body, int BodyLen0,  CUDAPrim_Struct* ColBody, int ColBodyLen0, float TimeStep)
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
				float num3 = (float)(Body[(i)].LocX - ColBody[(num)].LocX);
				float num4 = (float)(Body[(i)].LocY - ColBody[(num)].LocY);
				float num5 = num3 * num3 + num4 * num4;
				float num6 = (float)sqrt((double)num5);
				bool flag3 = (double)num6 <= ColBody[(num)].Size / 2.0 + Body[(i)].Size / 2.0;
				if (flag3)
				{
					ColBody[(num)].LastColID = i;
					bool flag4 = num6 > 0.0f;
					if (flag4)
					{
						float speedX = ColBody[(num)].SpeedX;
						float speedY = ColBody[(num)].SpeedY;
						float speedX2 = Body[(i)].SpeedX;
						float speedY2 = Body[(i)].SpeedY;
						float mass = ColBody[(num)].Mass;
						float mass2 = Body[(i)].Mass;
						float num7 = num3 / 2.0f;
						float num8 = num4 / 2.0f;
						num7 /= num6 / 2.0f;
						num8 /= num6 / 2.0f;
						float num9 = num7 * speedX + num8 * speedY;
						float num10 = num7 * speedX2 + num8 * speedY2;
						float num11 = (mass * num9 + mass2 * num10 - mass2 * (num9 - num10)) / (mass + mass2);
						float num12 = (mass * num9 + mass2 * num10 - mass * (num10 - num9)) / (mass + mass2);
						bool flag5 = ColBody[(num)].InRoche == 0 & Body[(i)].InRoche == 1;
						if (flag5)
						{
							bool flag6 = ColBody[(num)].Mass > Body[(i)].Mass;
							if (flag6)
							{
								float speedX3 = ColBody[(num)].SpeedX;
								float speedY3 = ColBody[(num)].SpeedY;
								ColBody[(num)].SpeedX = ColBody[(num)].SpeedX + (num11 - num9) * num7;
								ColBody[(num)].SpeedY = ColBody[(num)].SpeedY + (num11 - num9) * num8;
								float num13 = (float)(3.14159265358979 * pow(ColBody[(num)].Size, 2.0));
								float num14 = (float)(3.14159265358979 * pow(Body[(i)].Size, 2.0));
								num13 += num14;
								ColBody[(num)].Size = sqrt((double)num13 / 3.14159265358979);
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
										float speedX3 = ColBody[(num)].SpeedX;
										float speedY3 = ColBody[(num)].SpeedY;
										ColBody[(num)].SpeedX = ColBody[(num)].SpeedX + (num11 - num9) * num7;
										ColBody[(num)].SpeedY = ColBody[(num)].SpeedY + (num11 - num9) * num8;
										float num13 = (float)(3.14159265358979 * pow(ColBody[(num)].Size, 2.0));
										float num14 = (float)(3.14159265358979 * pow(Body[(i)].Size, 2.0));
										num13 += num14;
										ColBody[(num)].Size = sqrt((double)num13 / 3.14159265358979);
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
							bool flag9 = ColBody[(num)].InRoche == 0 & Body[(i)].InRoche == 0;
							if (flag9)
							{
								bool flag10 = ColBody[(num)].Mass > Body[(i)].Mass;
								if (flag10)
								{
									float speedX3 = ColBody[(num)].SpeedX;
									float speedY3 = ColBody[(num)].SpeedY;
									ColBody[(num)].SpeedX = ColBody[(num)].SpeedX + (num11 - num9) * num7;
									ColBody[(num)].SpeedY = ColBody[(num)].SpeedY + (num11 - num9) * num8;
									float num13 = (float)(3.14159265358979 * pow(ColBody[(num)].Size, 2.0));
									float num14 = (float)(3.14159265358979 * pow(Body[(i)].Size, 2.0));
									num13 += num14;
									ColBody[(num)].Size = sqrt((double)num13 / 3.14159265358979);
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
											float speedX3 = ColBody[(num)].SpeedX;
											float speedY3 = ColBody[(num)].SpeedY;
											ColBody[(num)].SpeedX = ColBody[(num)].SpeedX + (num11 - num9) * num7;
											ColBody[(num)].SpeedY = ColBody[(num)].SpeedY + (num11 - num9) * num8;
											float num13 = (float)(3.14159265358979 * pow(ColBody[(num)].Size, 2.0));
											float num14 = (float)(3.14159265358979 * pow(Body[(i)].Size, 2.0));
											num13 += num14;
											ColBody[(num)].Size = sqrt((double)num13 / 3.14159265358979);
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
									float num15 = mass * mass2;
									float num16 = (float)((double)num15 / ((double)(num6 * num6) + (ColBody[(num)].Size / 2.0 + Body[(i)].Size / 2.0)));
									float num17 = num16 * num3 / num6;
									float num18 = num16 * num4 / num6;
									int num19 = (int)rint(40.0 - sqrt((double)ColBody[(num)].Mass) * 2.0 - (double)(TimeStep * 1000.0f));
									int expr_895_cp_0_cp_1 = num;
									ColBody[(expr_895_cp_0_cp_1)].ForceX = ColBody[(expr_895_cp_0_cp_1)].ForceX - num17 * (float)num19;
									int expr_8B0_cp_0_cp_1 = num;
									ColBody[(expr_8B0_cp_0_cp_1)].ForceY = ColBody[(expr_8B0_cp_0_cp_1)].ForceY - num18 * (float)num19;
									double num20 = 0.5;
									int expr_8D6_cp_0_cp_1 = num;
									ColBody[(expr_8D6_cp_0_cp_1)].SpeedX = (float)((double)ColBody[(expr_8D6_cp_0_cp_1)].SpeedX + (double)((num11 - num9) * num7) * num20);
									int expr_8F8_cp_0_cp_1 = num;
									ColBody[(expr_8F8_cp_0_cp_1)].SpeedY = (float)((double)ColBody[(expr_8F8_cp_0_cp_1)].SpeedY + (double)((num11 - num9) * num8) * num20);
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
							float num13 = (float)(3.14159265358979 * pow(ColBody[(num)].Size, 2.0));
							float num14 = (float)(3.14159265358979 * pow(Body[(i)].Size, 2.0));
							num13 += num14;
							ColBody[(num)].Size = sqrt((double)num13 / 3.14159265358979);
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
		int expr_A44_cp_0_cp_1 = num;
		ColBody[(expr_A44_cp_0_cp_1)].SpeedX = ColBody[(expr_A44_cp_0_cp_1)].SpeedX + TimeStep * ColBody[(num)].ForceX / ColBody[(num)].Mass;
		int expr_A76_cp_0_cp_1 = num;
		ColBody[(expr_A76_cp_0_cp_1)].SpeedY = ColBody[(expr_A76_cp_0_cp_1)].SpeedY + TimeStep * ColBody[(num)].ForceY / ColBody[(num)].Mass;
		int expr_AA8_cp_0_cp_1 = num;
		ColBody[(expr_AA8_cp_0_cp_1)].LocX = ColBody[(expr_AA8_cp_0_cp_1)].LocX + (double)(TimeStep * ColBody[(num)].SpeedX);
		int expr_ACD_cp_0_cp_1 = num;
		ColBody[(expr_ACD_cp_0_cp_1)].LocY = ColBody[(expr_ACD_cp_0_cp_1)].LocY + (double)(TimeStep * ColBody[(num)].SpeedY);
	}
}
