"use client";
import { useState } from "react";
import { generateOTP, validateOTP } from "../services/otpService";
import Swal from "sweetalert2";
import Image from "next/image";
import "../styles/globals.css";

export default function OTPForm() {
  const [otp, setOTP] = useState("");

  /**
   * Displays a toast notification with a given message, icon, timer, and position.
   * @param {*} html - The HTML content to display in the toast.
   * @param {*} icon - The icon to display ("info", "error", "success" etc.).
   * @param {*} timer - The duration the toast should stay on screen.
   * @param {*} timerProgressBar - Whether to show the timer progress bar (true by default).
   * @param {*} position - The position of the toast on the screen (bottom, center, etc)
   */
  function notifyToast(
    html,
    icon,
    timer,
    timerProgressBar = true,
    position = "top"
  ) {
    Swal.fire({
      toast: true,
      position: position,
      icon: icon,
      html: html,
      showConfirmButton: false,
      timer: timer,
      timerProgressBar: timerProgressBar,
      customClass: {
        popup: "colored-toast",
      },
    });
  }

  /**
   * Handles the OTP generation calling the generateOTP service and displaying the OTP in a toast notification,
   * then calculates the expiration time for the OTP and displays it in the notification.
   */
  const handleGenerateOTP = async () => {
    try {
      const response = await generateOTP();
      const expiresIn =
        new Date(response.expirationTime).getTime() - Date.now();
      notifyToast(
        `<p>OTP: <strong>${response.otp}</strong></p>`,
        "info",
        expiresIn,
        true,
        "top"
      );
    } catch (err) {
      notifyToast(`<p>${err.message}</p>`, "error", 3000, true, "top");
    }
  };

  /**
   * Handles the OTP validation by calling validate OTP service and displaying the result in a toast notification.
   * @returns void
   */
  const handleValidateOTP = async () => {
    if (!otp) {
      notifyToast(`<p>OTP is required</p>`, "error", 3000, true, "top");
      return;
    }

    try {
      const response = await validateOTP(otp);
      notifyToast(`<p>${response.message}</p>`, "success", 3000, true, "top");
    } catch (err) {
      notifyToast(`<p>${err.message}</p>`, "error", 3000, true, "top");
    }
  };

  return (
    <div>
      <div className="grid grid-rows-[20px_1fr_20px] items-center justify-items-center min-h-screen p-8 pb-20 gap-16 sm:p-20 font-[family-name:var(--font-geist-sans)]">
        <h1>OTP SYSTEM</h1>
        <main className="flex flex-col gap-8 row-start-2 items-center sm:items-start">
          <div>
            <label className="input-label">OTP: </label>
            <input
              type="text"
              value={otp}
              onChange={(e) => setOTP(e.target.value)}
              placeholder="Enter OTP"
              className="input-field"
            />
          </div>
          <div className="flex gap-4 items-center flex-col sm:flex-row">
            <a
              className="rounded-full border border-solid border-black/[.08] dark:border-white/[.145] transition-colors flex items-center justify-center bg-foreground text-background gap-2 hover:bg-[#f2f2f2] dark:hover:bg-[#1a1a1a] text-sm sm:text-base h-10 sm:h-12 px-4 sm:px-5"
              target="_blank"
              rel="noopener noreferrer"
              onClick={handleGenerateOTP}
            >
              <Image
                className="dark:invert"
                src="/vercel.svg"
                alt="Vercel logomark"
                width={20}
                height={20}
              />
              Generate OTP
            </a>
            <a
              className="rounded-full border border-solid border-black/[.08] dark:border-white/[.145] transition-colors flex items-center justify-center hover:bg-[#f2f2f2] dark:hover:bg-[#1a1a1a] hover:border-transparent text-sm sm:text-base h-10 sm:h-12 px-4 sm:px-5 sm:min-w-44"
              target="_blank"
              rel="noopener noreferrer"
              onClick={handleValidateOTP}
            >
              Validate OTP
            </a>
          </div>
        </main>
      </div>
    </div>
  );
}
