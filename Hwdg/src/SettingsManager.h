// Copyright 2017 Oleg Petrochenko
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#pragma once
#include "PlatformDefinitions.h"
#include "Response.h"
#include <stdint.h>

#define SETTINGS_DEFAULT_0            ((uint8_t)0x1C)
#define SETTINGS_DEFAULT_1            ((uint8_t)0x44)
#define SETTINGS_DEFAULT_2            ((uint8_t)0x48)
#define SETTINGS_DEFAULT_3            ((uint8_t)0x00)

#define LED_DISABLED                  ((uint8_t)(1U << 0U))
#define EVENTS_ENABLED                ((uint8_t)(1U << 1U))
#define APPLY_SETTINGS_AT_STARTUP     ((uint8_t)(1U << 2U))
#define RST_PULSE_ENABLED             ((uint8_t)(1U << 3U))
#define PWR_PULSE_ENABLED             ((uint8_t)(1U << 4U))

/**
 * \brief Represents settings manager that saves and obtains settings stored in NVRAM.
 */
class SettingsManager
{
public:
	SettingsManager();
	_virtual ~SettingsManager()
	{
	}

	/**
	 * \brief Enable power pin pulse on chip power on.
	 * \return Returns operation status.
	 */
	_virtual Response PwrPulseOnStartupEnable();

	/**
	 * \brief Disable power pin pulse on chip power on.
	 * \return Returns operation status.
	 */
	_virtual Response PwrPulseOnStartupDisable();

	/**
	 * \brief Enable reset pin pulse on chip power on.
	 * \return Returns operation status.
	 */
	_virtual Response RstPulseOnStartupEnable();

	/**
	 * \brief Disable reset pin pulse on chip power on.
	 * \return Returns operation status.
	 */
	_virtual Response RstPulseOnStartupDisable();

	/**
	 * \brief Save user HWDG settings into NVRAM.
	 * \param status Status to be saved.
	 * \return Returns true if save operation succeeded, otherwise false.
	 */
	_virtual bool SaveUserSettings(uint32_t status);

	/**
	 * \brief Fetch user HWDG settings from NVRAM.
	 * \return Returns user settings.
	 */
	_virtual uint32_t ObtainUserSettings();

	/**
	 * \brief Apply user settings at startup.
	 * \return Returns operation status.
	 */
	_virtual Response ApplyUserSettingsAtStartup();

	/**
	 * \brief Load default settings at startup.
	 * \return Returns operation status.
	 */
	_virtual Response LoadDefaultSettingsAtStartup();

	/**
	 * \brief Restore factory settings.
	 * \return Returns operation statuss.
	 */
	_virtual bool RestoreFactory();

	/**
	 * \brief Get boot settings.
	 */
	_virtual uint8_t GetBootSettings();
};
